namespace CineReserve.Application.DTOs.ShowTime
{
    public class CreateShowTimeDto
    {
        public int MovieId { get; set; }
        public int TheaterHallId { get; set; }
        public DateTime StartTimeUtc { get; set; }
        public decimal BasePrice { get; set; }
    }
}
