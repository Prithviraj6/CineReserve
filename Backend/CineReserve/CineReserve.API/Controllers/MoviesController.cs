using CineReserve.Application.DTOs.Movie;
using CineReserve.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineReserve.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Get all movies (public).
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _movieService.GetAllMoviesAsync();
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Get a specific movie by ID (public).
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _movieService.GetMovieByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Add a new movie (Admin only).
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateMovieDto request)
        {
            var result = await _movieService.CreateMovieAsync(request);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Update a movie (Admin only).
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateMovieDto request)
        {
            var result = await _movieService.UpdateMovieAsync(id, request);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Delete a movie — soft delete (Admin only).
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _movieService.DeleteMovieAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
