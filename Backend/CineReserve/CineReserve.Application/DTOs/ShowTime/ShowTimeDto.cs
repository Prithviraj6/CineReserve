namespace CineReserve.Application.DTOs.ShowTime
{
    public class ShowTimeDto
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public int TheaterHallId { get; set; }
        public string TheaterHallName { get; set; } = string.Empty;
        public DateTime StartTimeUtc { get; set; }
        public decimal BasePrice { get; set; }
    }
}
