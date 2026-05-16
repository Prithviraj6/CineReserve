namespace CineReserve.Application.DTOs.Booking
{
    public class TicketDetailDto
    {
        public string RowLabel { get; set; } = string.Empty;
        public int SeatNumber { get; set; }
        public string SeatType { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
