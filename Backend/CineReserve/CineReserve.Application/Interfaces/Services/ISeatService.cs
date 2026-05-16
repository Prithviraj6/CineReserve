using CineReserve.Application.Common;
using CineReserve.Application.DTOs.Seat;

namespace CineReserve.Application.Interfaces.Services
{
    public interface ISeatService
    {
        /// <summary>
        /// Returns the full seat grid for a showtime, with availability status.
        /// Joins theater hall seats with sold ticket details.
        /// </summary>
        Task<ApiResponse<IEnumerable<SeatDto>>> GetSeatMapForShowtimeAsync(int showtimeId);
    }
}
