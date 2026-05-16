using CineReserve.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CineReserve.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeatsController : ControllerBase
    {
        private readonly ISeatService _seatService;

        public SeatsController(ISeatService seatService)
        {
            _seatService = seatService;
        }

        /// <summary>
        /// Get the real-time seat map for a showtime.
        /// Returns each seat with its availability status for the Angular seat grid.
        /// </summary>
        [HttpGet("{showtimeId}")]
        public async Task<IActionResult> GetSeatMap(int showtimeId)
        {
            var result = await _seatService.GetSeatMapForShowtimeAsync(showtimeId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
