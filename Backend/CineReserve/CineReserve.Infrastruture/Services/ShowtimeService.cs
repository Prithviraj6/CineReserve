using AutoMapper;
using CineReserve.Application.Common;
using CineReserve.Application.DTOs.ShowTime;
using CineReserve.Application.Interfaces.Repositories;
using CineReserve.Application.Interfaces.Services;
using CineReserve.Domain.Entities;

namespace CineReserve.Infrastruture.Services
{
    public class ShowtimeService : IShowtimeService
    {
        private readonly IShowtimeRepository _showtimeRepo;
        private readonly IMovieRepository _movieRepo;
        private readonly ITheaterHallRepository _hallRepo;
        private readonly IMapper _mapper;

        public ShowtimeService(
            IShowtimeRepository showtimeRepo,
            IMovieRepository movieRepo,
            ITheaterHallRepository hallRepo,
            IMapper mapper)
        {
            _showtimeRepo = showtimeRepo;
            _movieRepo = movieRepo;
            _hallRepo = hallRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<ShowTimeDto>>> GetShowtimesByMovieIdAsync(int movieId)
        {
            var movie = await _movieRepo.GetByIdAsync(movieId);
            if (movie == null)
                return ApiResponse<IEnumerable<ShowTimeDto>>.FailResponse("Movie not found", 404);

            var showtimes = await _showtimeRepo.GetShowtimesByMovieIdAsync(movieId);
            var dtos = _mapper.Map<IEnumerable<ShowTimeDto>>(showtimes);
            return ApiResponse<IEnumerable<ShowTimeDto>>.SuccessResponse(dtos);
        }

        public async Task<ApiResponse<ShowTimeDto>> CreateShowtimeAsync(CreateShowTimeDto request)
        {
            var movie = await _movieRepo.GetByIdAsync(request.MovieId);
            if (movie == null)
                return ApiResponse<ShowTimeDto>.FailResponse("Movie not found", 404);

            var hall = await _hallRepo.GetByIdAsync(request.TheaterHallId);
            if (hall == null)
                return ApiResponse<ShowTimeDto>.FailResponse("Theater hall not found", 404);

            if (request.StartTimeUtc <= DateTime.UtcNow)
                return ApiResponse<ShowTimeDto>.FailResponse("Showtime must be in the future");

            var showtime = _mapper.Map<Showtime>(request);
            await _showtimeRepo.AddAsync(showtime);
            await _showtimeRepo.SaveChangesAsync();

            // Reload with navigation properties for the response
            var created = await _showtimeRepo.GetShowtimeWithDetailsAsync(showtime.Id);
            var dto = _mapper.Map<ShowTimeDto>(created);
            return ApiResponse<ShowTimeDto>.SuccessResponse(dto, "Showtime scheduled", 201);
        }

        public async Task<ApiResponse<bool>> DeleteShowtimeAsync(int id)
        {
            var showtime = await _showtimeRepo.GetByIdAsync(id);
            if (showtime == null)
                return ApiResponse<bool>.FailResponse("Showtime not found", 404);

            _showtimeRepo.Delete(showtime);
            await _showtimeRepo.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true, "Showtime deleted");
        }
    }
}
