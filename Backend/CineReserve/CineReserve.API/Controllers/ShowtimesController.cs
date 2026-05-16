using CineReserve.Application.DTOs.ShowTime;
using CineReserve.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineReserve.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShowtimesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ShowtimesController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        /// <summary>
        /// Get showtimes for a specific movie (public).
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetByMovie([FromQuery] int movieId)
        {
            var result = await _scheduleService.GetShowtimesByMovieIdAsync(movieId);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Schedule a new showtime (Admin only).
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateShowTimeDto request)
        {
            var result = await _scheduleService.CreateShowtimeAsync(request);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Delete a showtime (Admin only).
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _scheduleService.DeleteShowtimeAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Get all theater halls (Admin only).
        /// </summary>
        [HttpGet("halls")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetHalls()
        {
            var result = await _scheduleService.GetAllTheaterHallsAsync();
            return StatusCode(result.StatusCode, result);
        }
    }
}
