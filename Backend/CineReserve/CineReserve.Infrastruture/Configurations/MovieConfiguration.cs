using CineReserve.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CineReserve.Infrastruture.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(m => m.Description)
                .HasMaxLength(2000);

            builder.Property(m => m.Genre)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.Language)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(m => m.PosterUrl)
                .HasMaxLength(500);

            builder.HasMany(m => m.Showtimes)
                .WithOne(s => s.Movie)
                .HasForeignKey(s => s.MovieId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
