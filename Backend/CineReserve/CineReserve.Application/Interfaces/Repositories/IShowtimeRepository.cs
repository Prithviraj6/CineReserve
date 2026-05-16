using CineReserve.Domain.Entities;

namespace CineReserve.Application.Interfaces.Repositories
{
    public interface IShowtimeRepository : IGenericRepository<Showtime>
    {
        Task<IEnumerable<Showtime>> GetShowtimesByMovieIdAsync(int movieId);
        Task<Showtime?> GetShowtimeWithDetailsAsync(int showtimeId);
    }
}
