using CineReserve.Domain.Entities;

namespace CineReserve.Application.Interfaces.Repositories
{
    public interface ISeatRepository : IGenericRepository<Seat>
    {
        Task<IEnumerable<Seat>> GetSeatsByTheaterHallIdAsync(int theaterHallId);
        Task<IEnumerable<Seat>> GetSeatsByIdsAsync(IEnumerable<int> seatIds);
    }
}
