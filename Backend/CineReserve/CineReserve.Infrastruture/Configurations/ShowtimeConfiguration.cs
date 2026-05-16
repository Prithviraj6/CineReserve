using CineReserve.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CineReserve.Infrastruture.Configurations
{
    public class ShowtimeConfiguration : IEntityTypeConfiguration<Showtime>
    {
        public void Configure(EntityTypeBuilder<Showtime> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.BasePrice)
                .HasColumnType("decimal(18,2)");

            builder.HasMany(s => s.Bookings)
                .WithOne(b => b.Showtime)
                .HasForeignKey(b => b.ShowtimeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.TicketDetails)
                .WithOne(t => t.Showtime)
                .HasForeignKey(t => t.ShowtimeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes for fast lookup when user switches showtimes
            builder.HasIndex(s => s.MovieId);
            builder.HasIndex(s => s.TheaterHallId);
        }
    }
}
