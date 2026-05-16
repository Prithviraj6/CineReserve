using CineReserve.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CineReserve.Infrastruture.Configurations
{
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.RowLabel)
                .IsRequired()
                .HasMaxLength(5);

            builder.HasMany(s => s.TicketDetails)
                .WithOne(t => t.Seat)
                .HasForeignKey(t => t.SeatId)
                .OnDelete(DeleteBehavior.Restrict);

            // Index for quick seat lookups by hall
            builder.HasIndex(s => s.TheaterHallId);
        }
    }
}
