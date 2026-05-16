using CineReserve.Application.Common;
using CineReserve.Application.DTOs.Admin;

namespace CineReserve.Application.Interfaces.Services
{
    public interface IAdminService
    {
        /// <summary>
        /// Calculates occupancy percentage and total revenue for a movie across all showtimes.
        /// </summary>
        Task<ApiResponse<MovieStatsDto>> GetMovieStatsAsync(int movieId);
    }
}
