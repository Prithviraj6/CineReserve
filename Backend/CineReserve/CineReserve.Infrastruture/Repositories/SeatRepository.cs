using CineReserve.Application.Interfaces.Repositories;
using CineReserve.Domain.Entities;
using CineReserve.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace CineReserve.Infrastruture.Repositories
{
    public class SeatRepository : GenericRepository<Seat>, ISeatRepository
    {
        public SeatRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Seat>> GetSeatsByTheaterHallIdAsync(int theaterHallId)
        {
            return await _dbSet
                .Where(s => s.TheaterHallId == theaterHallId)
                .OrderBy(s => s.RowLabel)
                .ThenBy(s => s.SeatNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<Seat>> GetSeatsByIdsAsync(IEnumerable<int> seatIds)
        {
            return await _dbSet
                .Where(s => seatIds.Contains(s.Id))
                .ToListAsync();
        }
    }
}
