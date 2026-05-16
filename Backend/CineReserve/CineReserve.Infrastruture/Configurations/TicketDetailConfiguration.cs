using CineReserve.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CineReserve.Infrastruture.Configurations
{
    public class TicketDetailConfiguration : IEntityTypeConfiguration<TicketDetail>
    {
        public void Configure(EntityTypeBuilder<TicketDetail> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.SeatPrice)
                .HasColumnType("decimal(18,2)");

            // CRITICAL: Composite unique constraint — database-level hard stop
            // Guarantees a seat cannot be sold twice for the same showtime.
            // This is the ultimate safety net even if the application-level
            // concurrency control somehow fails.
            builder.HasIndex(t => new { t.ShowtimeId, t.SeatId })
                .IsUnique()
                .HasDatabaseName("IX_TicketDetail_Showtime_Seat_Unique");

            builder.HasIndex(t => t.ShowtimeId);
        }
    }
}
