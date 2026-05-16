using CineReserve.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineReserve.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        /// <summary>
        /// Get occupancy percentage and total revenue for a specific movie.
        /// </summary>
        [HttpGet("stats")]
        public async Task<IActionResult> GetMovieStats([FromQuery] int movieId)
        {
            var result = await _adminService.GetMovieStatsAsync(movieId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
