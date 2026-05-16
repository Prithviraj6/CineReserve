namespace CineReserve.Application.DTOs.Seat
{
    public class SeatDto
    {
        public int SeatId { get; set; }
        public string RowLabel { get; set; } = string.Empty;
        public int SeatNumber { get; set; }
        public string SeatType { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
    }
}
