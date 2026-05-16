using CineReserve.Domain.Entities;

namespace CineReserve.Application.Interfaces.Repositories
{
    public interface ITicketDetailRepository : IGenericRepository<TicketDetail>
    {
        Task<IEnumerable<TicketDetail>> GetSoldSeatsByShowtimeIdAsync(int showtimeId);
        Task<bool> AreSeatsTakenAsync(int showtimeId, IEnumerable<int> seatIds);
    }
}
