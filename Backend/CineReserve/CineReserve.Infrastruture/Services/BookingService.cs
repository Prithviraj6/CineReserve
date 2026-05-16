using AutoMapper;
using CineReserve.Application.Common;
using CineReserve.Application.DTOs.Booking;
using CineReserve.Application.Interfaces.Repositories;
using CineReserve.Application.Interfaces.Services;
using CineReserve.Domain.Entities;
using CineReserve.Domain.Enums;
using CineReserve.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CineReserve.Infrastruture.Services
{
    public class BookingService : IBookingService
    {
        private readonly AppDbContext _context;
        private readonly IBookingRepository _bookingRepo;
        private readonly ITicketDetailRepository _ticketDetailRepo;
        private readonly ISeatRepository _seatRepo;
        private readonly IShowtimeRepository _showtimeRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public BookingService(
            AppDbContext context,
            IBookingRepository bookingRepo,
            ITicketDetailRepository ticketDetailRepo,
            ISeatRepository seatRepo,
            IShowtimeRepository showtimeRepo,
            IUserRepository userRepo,
            IMapper mapper)
        {
            _context = context;
            _bookingRepo = bookingRepo;
            _ticketDetailRepo = ticketDetailRepo;
            _seatRepo = seatRepo;
            _showtimeRepo = showtimeRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Core booking engine with Serializable transaction isolation.
        /// Flow:
        ///   1. Begin Serializable transaction
        ///   2. Lock and check if requested seats are already sold for this showtime
        ///   3. Validate seats belong to the correct theater hall
        ///   4. Calculate total price (base price × seat type multiplier)
        ///   5. Check user credit balance
        ///   6. Deduct balance, create Booking + TicketDetails
        ///   7. Commit (or rollback on any failure)
        /// </summary>
        public async Task<ApiResponse<BookingDto>> CreateBookingAsync(int userId, CreateBookingDto request)
        {
            // Use Serializable isolation to prevent two users from booking the same seat
            await using var transaction = await _context.Database
                .BeginTransactionAsync(IsolationLevel.Serializable);

            try
            {
                // 1. Validate the showtime exists
                var showtime = await _showtimeRepo.GetShowtimeWithDetailsAsync(request.ShowtimeId);
                if (showtime == null)
                    return ApiResponse<BookingDto>.FailResponse("Showtime not found", 404);

                if (showtime.StartTimeUtc <= DateTime.UtcNow)
                    return ApiResponse<BookingDto>.FailResponse("Cannot book for a past showtime");

                // 2. Get the requested seat IDs
                var seatIds = request.Seats.Select(s => s.SeatId).ToList();

                // 3. Check for duplicate seats using raw SQL with row lock hint
                // This is the pessimistic concurrency control — it acquires exclusive row locks
                var parameters = new List<object> { request.ShowtimeId };
                var paramNames = new List<string>();
                for (int i = 0; i < seatIds.Count; i++)
                {
                    paramNames.Add($"{{{i + 1}}}");
                    parameters.Add(seatIds[i]);
                }

                var sql = $"SELECT * FROM TicketDetails WITH (XLOCK, ROWLOCK) WHERE ShowtimeId = {{0}} AND SeatId IN ({string.Join(",", paramNames)})";

                var takenSeatIds = await _context.TicketDetails
                    .FromSqlRaw(sql, parameters.ToArray())
                    .Select(t => t.SeatId)
                    .ToListAsync();

                if (takenSeatIds.Any())
                {
                    await transaction.RollbackAsync();
                    return ApiResponse<BookingDto>.FailResponse(
                        $"Seat(s) already reserved: {string.Join(", ", takenSeatIds)}. Please select different seats.");
                }

                // 4. Validate seats belong to the correct theater hall
                var seats = await _seatRepo.GetSeatsByIdsAsync(seatIds);
                var seatList = seats.ToList();

                if (seatList.Count != seatIds.Count)
                    return ApiResponse<BookingDto>.FailResponse("One or more selected seats are invalid");

                if (seatList.Any(s => s.TheaterHallId != showtime.TheaterHallId))
                    return ApiResponse<BookingDto>.FailResponse("Selected seats do not belong to this theater hall");

                // 5. Calculate total price
                decimal totalAmount = 0;
                foreach (var seat in seatList)
                {
                    decimal multiplier = seat.SeatType == SeatType.VIP ? 1.5m : 1.0m;
                    totalAmount += showtime.BasePrice * multiplier;
                }

                // 6. Check and deduct user credit balance
                var user = await _userRepo.GetByIdAsync(userId);
                if (user == null)
                    return ApiResponse<BookingDto>.FailResponse("User not found", 404);

                if (user.CreditBalance < totalAmount)
                    return ApiResponse<BookingDto>.FailResponse(
                        $"Insufficient balance. Required: {totalAmount:C}, Available: {user.CreditBalance:C}");

                user.CreditBalance -= totalAmount;
                _userRepo.Update(user);

                // 7. Create booking with unique reference
                var booking = new Booking
                {
                    UserId = userId,
                    ShowtimeId = request.ShowtimeId,
                    BookingReference = GenerateBookingReference(),
                    TotalAmount = totalAmount,
                    BookedAtUtc = DateTime.UtcNow
                };
                await _bookingRepo.AddAsync(booking);
                await _bookingRepo.SaveChangesAsync(); // Need the booking ID

                // 8. Create ticket details for each seat
                foreach (var seat in seatList)
                {
                    decimal multiplier = seat.SeatType == SeatType.VIP ? 1.5m : 1.0m;
                    var ticketDetail = new TicketDetail
                    {
                        BookingId = booking.Id,
                        ShowtimeId = request.ShowtimeId,
                        SeatId = seat.Id,
                        SeatPrice = showtime.BasePrice * multiplier
                    };
                    await _ticketDetailRepo.AddAsync(ticketDetail);
                }

                await _ticketDetailRepo.SaveChangesAsync();

                // 9. Commit the transaction
                await transaction.CommitAsync();

                // 10. Return the booking details
                var bookingWithDetails = await _bookingRepo.GetBookingWithDetailsAsync(booking.Id);
                var dto = _mapper.Map<BookingDto>(bookingWithDetails);
                return ApiResponse<BookingDto>.SuccessResponse(dto, "Booking confirmed!", 201);
            }
            catch (DbUpdateException)
            {
                await transaction.RollbackAsync();
                // The composite unique constraint caught a concurrent booking
                return ApiResponse<BookingDto>.FailResponse(
                    "Seat already reserved by another user. Please try again with different seats.");
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw; // Let the global exception handler deal with unexpected errors
            }
        }

        public async Task<ApiResponse<IEnumerable<BookingDto>>> GetUserBookingsAsync(int userId)
        {
            var bookings = await _bookingRepo.GetBookingsByUserIdAsync(userId);
            var dtos = _mapper.Map<IEnumerable<BookingDto>>(bookings);
            return ApiResponse<IEnumerable<BookingDto>>.SuccessResponse(dtos);
        }

        public async Task<ApiResponse<BookingDto>> GetBookingByIdAsync(int bookingId, int userId)
        {
            var booking = await _bookingRepo.GetBookingWithDetailsAsync(bookingId);
            if (booking == null || booking.UserId != userId)
                return ApiResponse<BookingDto>.FailResponse("Booking not found", 404);

            var dto = _mapper.Map<BookingDto>(booking);
            return ApiResponse<BookingDto>.SuccessResponse(dto);
        }

        private static string GenerateBookingReference()
        {
            return $"CR-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
        }
    }
}
