using CineReserve.Application.DTOs.ShowTime;
using CineReserve.Application.DTOs.TheaterHall;

namespace CineReserve.Application.Interfaces.Services
{
    public interface IScheduleService
    {
        Task<ApiResponse<IEnumerable<ShowTimeDto>>> GetShowtimesByMovieIdAsync(int movieId);
        Task<ApiResponse<ShowTimeDto>> CreateShowtimeAsync(CreateShowTimeDto request);
        Task<ApiResponse<bool>> DeleteShowtimeAsync(int id);
        Task<ApiResponse<IEnumerable<TheaterHallDto>>> GetAllTheaterHallsAsync();
    }
}
