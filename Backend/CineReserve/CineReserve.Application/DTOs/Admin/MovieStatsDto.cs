namespace CineReserve.Application.DTOs.Admin
{
    public class MovieStatsDto
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public int TotalSeatsAvailable { get; set; }
        public int TotalSeatsSold { get; set; }
        public double OccupancyPercentage { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalShowtimes { get; set; }
    }
}
