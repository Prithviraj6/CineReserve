using AutoMapper;
using CineReserve.Application.Common;
using CineReserve.Application.DTOs.Movie;
using CineReserve.Application.Interfaces.Repositories;
using CineReserve.Application.Interfaces.Services;
using CineReserve.Domain.Entities;

namespace CineReserve.Infrastruture.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepo;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepo, IMapper mapper)
        {
            _movieRepo = movieRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<MovieDto>>> GetAllMoviesAsync()
        {
            var movies = await _movieRepo.GetAllMoviesAsync();
            var dtos = _mapper.Map<IEnumerable<MovieDto>>(movies);
            return ApiResponse<IEnumerable<MovieDto>>.SuccessResponse(dtos);
        }

        public async Task<ApiResponse<MovieDto>> GetMovieByIdAsync(int id)
        {
            var movie = await _movieRepo.GetByIdAsync(id);
            if (movie == null)
                return ApiResponse<MovieDto>.FailResponse("Movie not found", 404);

            var dto = _mapper.Map<MovieDto>(movie);
            return ApiResponse<MovieDto>.SuccessResponse(dto);
        }

        public async Task<ApiResponse<MovieDto>> CreateMovieAsync(CreateMovieDto request)
        {
            var movie = _mapper.Map<Movie>(request);
            await _movieRepo.AddAsync(movie);
            await _movieRepo.SaveChangesAsync();

            var dto = _mapper.Map<MovieDto>(movie);
            return ApiResponse<MovieDto>.SuccessResponse(dto, "Movie created", 201);
        }

        public async Task<ApiResponse<MovieDto>> UpdateMovieAsync(int id, CreateMovieDto request)
        {
            var movie = await _movieRepo.GetByIdAsync(id);
            if (movie == null)
                return ApiResponse<MovieDto>.FailResponse("Movie not found", 404);

            movie.Title = request.Title;
            movie.Description = request.Description;
            movie.DurationMinutes = request.DurationMinutes;
            movie.Genre = request.Genre;
            movie.Language = request.Language;
            movie.PosterUrl = request.PosterUrl;
            movie.ReleaseDate = request.ReleaseDate;

            _movieRepo.Update(movie);
            await _movieRepo.SaveChangesAsync();

            var dto = _mapper.Map<MovieDto>(movie);
            return ApiResponse<MovieDto>.SuccessResponse(dto, "Movie updated");
        }

        public async Task<ApiResponse<bool>> DeleteMovieAsync(int id)
        {
            var movie = await _movieRepo.GetByIdAsync(id);
            if (movie == null)
                return ApiResponse<bool>.FailResponse("Movie not found", 404);

            _movieRepo.Delete(movie); // Soft delete via SaveChangesAsync interceptor
            await _movieRepo.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true, "Movie deleted");
        }
    }
}
