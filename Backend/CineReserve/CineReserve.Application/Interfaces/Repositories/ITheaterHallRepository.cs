using CineReserve.Domain.Entities;

namespace CineReserve.Application.Interfaces.Repositories
{
    public interface ITheaterHallRepository : IGenericRepository<TheaterHall>
    {
        Task<TheaterHall?> GetHallWithSeatsAsync(int hallId);
    }
}
