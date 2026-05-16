namespace CineReserve.Application.DTOs.Booking
{
    public class CreateBookingDto
    {
        public int ShowtimeId { get; set; }
        public List<SeatSelectionDto> Seats { get; set; } = new();
    }
}
