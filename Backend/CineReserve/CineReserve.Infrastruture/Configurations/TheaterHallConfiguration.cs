using CineReserve.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CineReserve.Infrastruture.Configurations
{
    public class TheaterHallConfiguration : IEntityTypeConfiguration<TheaterHall>
    {
        public void Configure(EntityTypeBuilder<TheaterHall> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(h => h.Seats)
                .WithOne(s => s.TheaterHall)
                .HasForeignKey(s => s.TheaterHallId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(h => h.Showtimes)
                .WithOne(s => s.TheaterHall)
                .HasForeignKey(s => s.TheaterHallId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
