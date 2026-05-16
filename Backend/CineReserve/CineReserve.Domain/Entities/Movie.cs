using CineReserve.Domain.Common;
namespace CineReserve.Domain.Entities
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int DurationMinutes { get; set; }

        public string Genre { get; set; } = string.Empty;

        public string Language { get; set; } = string.Empty;

        public string PosterUrl { get; set; } = string.Empty;

        public DateTime ReleaseDate { get; set; }

        public ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
    }
}
