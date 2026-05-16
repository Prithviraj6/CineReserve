using CineReserve.Application.DTOs.Booking;
using CineReserve.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CineReserve.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>
        /// Book seats for a showtime (authenticated users only).
        /// Uses transactional concurrency control to prevent double-booking.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingDto request)
        {
            var userId = GetCurrentUserId();
            var result = await _bookingService.CreateBookingAsync(userId, request);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Get the current user's booking history.
        /// </summary>
        [HttpGet("mine")]
        public async Task<IActionResult> GetMyBookings()
        {
            var userId = GetCurrentUserId();
            var result = await _bookingService.GetUserBookingsAsync(userId);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Get a specific booking by ID (must belong to the current user).
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = GetCurrentUserId();
            var result = await _bookingService.GetBookingByIdAsync(id, userId);
            return StatusCode(result.StatusCode, result);
        }

        private int GetCurrentUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null ? int.Parse(claim.Value) : 0;
        }
    }
}
