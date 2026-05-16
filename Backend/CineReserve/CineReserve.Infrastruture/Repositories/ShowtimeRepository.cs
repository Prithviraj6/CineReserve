using CineReserve.Application.Interfaces.Repositories;
using CineReserve.Domain.Entities;
using CineReserve.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace CineReserve.Infrastruture.Repositories
{
    public class ShowtimeRepository : GenericRepository<Showtime>, IShowtimeRepository
    {
        public ShowtimeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Showtime>> GetShowtimesByMovieIdAsync(int movieId)
        {
            return await _dbSet
                .Include(s => s.Movie)
                .Include(s => s.TheaterHall)
                .Where(s => s.MovieId == movieId && s.StartTimeUtc >= DateTime.UtcNow)
                .OrderBy(s => s.StartTimeUtc)
                .ToListAsync();
        }

        public async Task<Showtime?> GetShowtimeWithDetailsAsync(int showtimeId)
        {
            return await _dbSet
                .Include(s => s.Movie)
                .Include(s => s.TheaterHall)
                    .ThenInclude(h => h.Seats)
                .FirstOrDefaultAsync(s => s.Id == showtimeId);
        }
    }
}
