using CineReserve.Application.Interfaces.Repositories;
using CineReserve.Domain.Entities;
using CineReserve.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace CineReserve.Infrastruture.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Movie?> GetMovieWithShowtimesAsync(int movieId)
        {
            return await _dbSet
                .Include(m => m.Showtimes)
                    .ThenInclude(s => s.TheaterHall)
                .FirstOrDefaultAsync(m => m.Id == movieId);
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _dbSet.OrderByDescending(m => m.ReleaseDate).ToListAsync();
        }
    }
}
