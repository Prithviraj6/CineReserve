using CineReserve.Domain.Entities;

namespace CineReserve.Application.Interfaces.Repositories
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int userId);
        Task<Booking?> GetBookingWithDetailsAsync(int bookingId);
    }
}
