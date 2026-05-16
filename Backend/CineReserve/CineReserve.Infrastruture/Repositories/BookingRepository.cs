using CineReserve.Application.Interfaces.Repositories;
using CineReserve.Domain.Entities;
using CineReserve.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace CineReserve.Infrastruture.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int userId)
        {
            return await _dbSet
                .Include(b => b.Showtime)
                    .ThenInclude(s => s.Movie)
                .Include(b => b.Showtime)
                    .ThenInclude(s => s.TheaterHall)
                .Include(b => b.TicketDetails)
                    .ThenInclude(t => t.Seat)
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.BookedAtUtc)
                .ToListAsync();
        }

        public async Task<Booking?> GetBookingWithDetailsAsync(int bookingId)
        {
            return await _dbSet
                .Include(b => b.Showtime)
                    .ThenInclude(s => s.Movie)
                .Include(b => b.Showtime)
                    .ThenInclude(s => s.TheaterHall)
                .Include(b => b.TicketDetails)
                    .ThenInclude(t => t.Seat)
                .FirstOrDefaultAsync(b => b.Id == bookingId);
        }
    }
}
