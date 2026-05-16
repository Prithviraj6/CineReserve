namespace CineReserve.Application.DTOs.Booking
{
    public class BookingDto
    {
        public int Id { get; set; }
        public string BookingReference { get; set; } = string.Empty;
        public string MovieTitle { get; set; } = string.Empty;
        public string TheaterHallName { get; set; } = string.Empty;
        public DateTime ShowTimeUtc { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime BookedAtUtc { get; set; }
        public List<TicketDetailDto> Tickets { get; set; } = new();
    }
}
