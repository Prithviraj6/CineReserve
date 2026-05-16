using CineReserve.Application.Interfaces.Repositories;
using CineReserve.Domain.Entities;
using CineReserve.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace CineReserve.Infrastruture.Repositories
{
    public class TheaterHallRepository : GenericRepository<TheaterHall>, ITheaterHallRepository
    {
        public TheaterHallRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<TheaterHall?> GetHallWithSeatsAsync(int hallId)
        {
            return await _dbSet
                .Include(h => h.Seats)
                .FirstOrDefaultAsync(h => h.Id == hallId);
        }
    }
}
