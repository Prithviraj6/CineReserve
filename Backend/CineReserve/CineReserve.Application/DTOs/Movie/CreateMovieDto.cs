namespace CineReserve.Application.DTOs.Movie
{
    public class CreateMovieDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
    }
}
