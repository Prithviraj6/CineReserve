using CineReserve.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CineReserve.Infrastruture.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.BookingReference)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(b => b.BookingReference)
                .IsUnique();

            builder.Property(b => b.TotalAmount)
                .HasColumnType("decimal(18,2)");

            builder.HasMany(b => b.TicketDetails)
                .WithOne(t => t.Booking)
                .HasForeignKey(t => t.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(b => b.UserId);
        }
    }
}
