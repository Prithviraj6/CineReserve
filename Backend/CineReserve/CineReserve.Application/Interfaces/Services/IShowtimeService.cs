using CineReserve.Application.Common;
using CineReserve.Application.DTOs.ShowTime;

namespace CineReserve.Application.Interfaces.Services
{
    public interface IShowtimeService
    {
        Task<ApiResponse<IEnumerable<ShowTimeDto>>> GetShowtimesByMovieIdAsync(int movieId);
        Task<ApiResponse<ShowTimeDto>> CreateShowtimeAsync(CreateShowTimeDto request);
        Task<ApiResponse<bool>> DeleteShowtimeAsync(int id);
    }
}
