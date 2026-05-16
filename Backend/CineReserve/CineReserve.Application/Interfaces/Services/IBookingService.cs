using CineReserve.Application.Common;
using CineReserve.Application.DTOs.Booking;

namespace CineReserve.Application.Interfaces.Services
{
    public interface IBookingService
    {
        /// <summary>
        /// Books seats with transactional concurrency control.
        /// Uses Serializable isolation + row locking to prevent double-booking.
        /// </summary>
        Task<ApiResponse<BookingDto>> CreateBookingAsync(int userId, CreateBookingDto request);
        Task<ApiResponse<IEnumerable<BookingDto>>> GetUserBookingsAsync(int userId);
        Task<ApiResponse<BookingDto>> GetBookingByIdAsync(int bookingId, int userId);
    }
}
