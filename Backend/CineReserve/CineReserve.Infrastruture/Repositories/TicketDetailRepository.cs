using CineReserve.Application.Interfaces.Repositories;
using CineReserve.Domain.Entities;
using CineReserve.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace CineReserve.Infrastruture.Repositories
{
    public class TicketDetailRepository : GenericRepository<TicketDetail>, ITicketDetailRepository
    {
        public TicketDetailRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TicketDetail>> GetSoldSeatsByShowtimeIdAsync(int showtimeId)
        {
            return await _dbSet
                .Where(t => t.ShowtimeId == showtimeId)
                .ToListAsync();
        }

        public async Task<bool> AreSeatsTakenAsync(int showtimeId, IEnumerable<int> seatIds)
        {
            return await _dbSet
                .AnyAsync(t => t.ShowtimeId == showtimeId && seatIds.Contains(t.SeatId));
        }
    }
}
