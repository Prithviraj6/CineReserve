using CineReserve.Application.Common;
using CineReserve.Application.DTOs.Seat;
using CineReserve.Application.Interfaces.Repositories;
using CineReserve.Application.Interfaces.Services;

namespace CineReserve.Infrastruture.Services
{
    public class SeatService : ISeatService
    {
        private readonly IShowtimeRepository _showtimeRepo;
        private readonly ISeatRepository _seatRepo;
        private readonly ITicketDetailRepository _ticketDetailRepo;

        public SeatService(
            IShowtimeRepository showtimeRepo,
            ISeatRepository seatRepo,
            ITicketDetailRepository ticketDetailRepo)
        {
            _showtimeRepo = showtimeRepo;
            _seatRepo = seatRepo;
            _ticketDetailRepo = ticketDetailRepo;
        }

        /// <summary>
        /// Builds the real-time seat map for a showtime.
        /// Joins theater hall seats with sold ticket details to determine availability.
        /// </summary>
        public async Task<ApiResponse<IEnumerable<SeatDto>>> GetSeatMapForShowtimeAsync(int showtimeId)
        {
            var showtime = await _showtimeRepo.GetShowtimeWithDetailsAsync(showtimeId);
            if (showtime == null)
                return ApiResponse<IEnumerable<SeatDto>>.FailResponse("Showtime not found", 404);

            // Get all seats for the theater hall
            var seats = await _seatRepo.GetSeatsByTheaterHallIdAsync(showtime.TheaterHallId);

            // Get sold seats for this showtime
            var soldTickets = await _ticketDetailRepo.GetSoldSeatsByShowtimeIdAsync(showtimeId);
            var soldSeatIds = new HashSet<int>(soldTickets.Select(t => t.SeatId));

            // Build the seat map with availability
            var seatDtos = seats.Select(seat =>
            {
                decimal multiplier = seat.SeatType == Domain.Enums.SeatType.VIP ? 1.5m : 1.0m;
                return new SeatDto
                {
                    SeatId = seat.Id,
                    RowLabel = seat.RowLabel,
                    SeatNumber = seat.SeatNumber,
                    SeatType = seat.SeatType.ToString(),
                    Price = showtime.BasePrice * multiplier,
                    IsAvailable = !soldSeatIds.Contains(seat.Id),
                    MovieTitle = showtime.Movie.Title,
                    TheaterHallName = showtime.TheaterHall.Name
                };
            });

            return ApiResponse<IEnumerable<SeatDto>>.SuccessResponse(seatDtos);
        }
    }
}
