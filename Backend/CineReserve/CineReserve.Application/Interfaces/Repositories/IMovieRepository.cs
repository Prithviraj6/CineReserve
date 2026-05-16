using CineReserve.Domain.Entities;

namespace CineReserve.Application.Interfaces.Repositories
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<Movie?> GetMovieWithShowtimesAsync(int movieId);
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
    }
}
