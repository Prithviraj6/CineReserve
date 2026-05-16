using CineReserve.Application.Common;
using CineReserve.Application.DTOs.Admin;
using CineReserve.Application.Interfaces.Repositories;
using CineReserve.Application.Interfaces.Services;
using CineReserve.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace CineReserve.Infrastruture.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _context;
        private readonly IMovieRepository _movieRepo;

        public AdminService(AppDbContext context, IMovieRepository movieRepo)
        {
            _context = context;
            _movieRepo = movieRepo;
        }

        /// <summary>
        /// Calculates occupancy percentage and total revenue for a movie across all showtimes.
        /// Uses a complex join query across Showtimes, TheaterHalls, Seats, and TicketDetails.
        /// </summary>
        public async Task<ApiResponse<MovieStatsDto>> GetMovieStatsAsync(int movieId)
        {
            var movie = await _movieRepo.GetByIdAsync(movieId);
            if (movie == null)
                return ApiResponse<MovieStatsDto>.FailResponse("Movie not found", 404);

            // Get all showtimes for this movie
            var showtimes = await _context.Showtimes
                .Where(s => s.MovieId == movieId)
                .Include(s => s.TheaterHall)
                .ToListAsync();

            if (!showtimes.Any())
            {
                return ApiResponse<MovieStatsDto>.SuccessResponse(new MovieStatsDto
                {
                    MovieId = movieId,
                    MovieTitle = movie.Title,
                    TotalSeatsAvailable = 0,
                    TotalSeatsSold = 0,
                    OccupancyPercentage = 0,
                    TotalRevenue = 0,
                    TotalShowtimes = 0
                });
            }

            // Calculate total available seats across all showtimes
            // Each showtime has (TotalRows × SeatsPerRow) seats available
            int totalSeatsAvailable = showtimes.Sum(s => s.TheaterHall.TotalRows * s.TheaterHall.SeatsPerRow);

            // Get total sold tickets and revenue
            var showtimeIds = showtimes.Select(s => s.Id).ToList();
            var ticketStats = await _context.TicketDetails
                .Where(t => showtimeIds.Contains(t.ShowtimeId))
                .GroupBy(t => 1)
                .Select(g => new
                {
                    TotalSeatsSold = g.Count(),
                    TotalRevenue = g.Sum(t => t.SeatPrice)
                })
                .FirstOrDefaultAsync();

            int totalSeatsSold = ticketStats?.TotalSeatsSold ?? 0;
            decimal totalRevenue = ticketStats?.TotalRevenue ?? 0;
            double occupancyPercentage = totalSeatsAvailable > 0
                ? Math.Round((double)totalSeatsSold / totalSeatsAvailable * 100, 2)
                : 0;

            var stats = new MovieStatsDto
            {
                MovieId = movieId,
                MovieTitle = movie.Title,
                TotalSeatsAvailable = totalSeatsAvailable,
                TotalSeatsSold = totalSeatsSold,
                OccupancyPercentage = occupancyPercentage,
                TotalRevenue = totalRevenue,
                TotalShowtimes = showtimes.Count
            };

            return ApiResponse<MovieStatsDto>.SuccessResponse(stats);
        }
    }
}
