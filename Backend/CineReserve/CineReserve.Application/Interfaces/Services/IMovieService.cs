using CineReserve.Application.Common;
using CineReserve.Application.DTOs.Movie;

namespace CineReserve.Application.Interfaces.Services
{
    public interface IMovieService
    {
        Task<ApiResponse<IEnumerable<MovieDto>>> GetAllMoviesAsync();
        Task<ApiResponse<MovieDto>> GetMovieByIdAsync(int id);
        Task<ApiResponse<MovieDto>> CreateMovieAsync(CreateMovieDto request);
        Task<ApiResponse<MovieDto>> UpdateMovieAsync(int id, CreateMovieDto request);
        Task<ApiResponse<bool>> DeleteMovieAsync(int id);
    }
}
