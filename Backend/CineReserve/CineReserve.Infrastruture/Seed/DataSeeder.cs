using CineReserve.Domain.Entities;
using CineReserve.Domain.Enums;
using CineReserve.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace CineReserve.Infrastruture.Seed
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Only seed if the database is empty
            if (await context.Users.AnyAsync())
                return;

            // Seed Admin user
            var admin = new User
            {
                FullName = "Admin User",
                Email = "admin@cinereserve.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                CreditBalance = 99999m,
                Role = UserRole.Admin
            };
            context.Users.Add(admin);

            // Seed a regular user for testing
            var testUser = new User
            {
                FullName = "Test User",
                Email = "user@cinereserve.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("User@123"),
                CreditBalance = 5000m,
                Role = UserRole.User
            };
            context.Users.Add(testUser);

            // Seed Theater Halls
            var hall1 = new TheaterHall { Name = "Screen 1 - Standard", TotalRows = 8, SeatsPerRow = 10 };
            var hall2 = new TheaterHall { Name = "Screen 2 - Premium", TotalRows = 6, SeatsPerRow = 8 };
            context.TheaterHalls.AddRange(hall1, hall2);
            await context.SaveChangesAsync();

            // Seed Seats for Hall 1 (8 rows × 10 seats)
            var rowLabels = new[] { "A", "B", "C", "D", "E", "F", "G", "H" };
            for (int r = 0; r < hall1.TotalRows; r++)
            {
                for (int s = 1; s <= hall1.SeatsPerRow; s++)
                {
                    // Last 2 rows (G, H) are VIP
                    var seatType = r >= 6 ? SeatType.VIP : SeatType.Regular;
                    context.Seats.Add(new Seat
                    {
                        TheaterHallId = hall1.Id,
                        RowLabel = rowLabels[r],
                        SeatNumber = s,
                        SeatType = seatType
                    });
                }
            }

            // Seed Seats for Hall 2 (6 rows × 8 seats)
            var rowLabels2 = new[] { "A", "B", "C", "D", "E", "F" };
            for (int r = 0; r < hall2.TotalRows; r++)
            {
                for (int s = 1; s <= hall2.SeatsPerRow; s++)
                {
                    var seatType = r >= 4 ? SeatType.VIP : SeatType.Regular;
                    context.Seats.Add(new Seat
                    {
                        TheaterHallId = hall2.Id,
                        RowLabel = rowLabels2[r],
                        SeatNumber = s,
                        SeatType = seatType
                    });
                }
            }

            // Seed Movies
            var movie1 = new Movie
            {
                Title = "Interstellar",
                Description = "A group of explorers travel through a wormhole in space in an attempt to ensure humanity's survival.",
                DurationMinutes = 169,
                Genre = "Sci-Fi",
                Language = "English",
                PosterUrl = "https://image.tmdb.org/t/p/w500/gEU2QniE6E77NI6lCU6MxlNBvIx.jpg",
                ReleaseDate = new DateTime(2014, 11, 7)
            };

            var movie2 = new Movie
            {
                Title = "The Dark Knight",
                Description = "When the menace known as the Joker wreaks havoc on Gotham, Batman must face one of the greatest tests.",
                DurationMinutes = 152,
                Genre = "Action",
                Language = "English",
                PosterUrl = "https://image.tmdb.org/t/p/w500/qJ2tW6WMUDux911BTUgMe1uS2yg.jpg",
                ReleaseDate = new DateTime(2008, 7, 18)
            };

            var movie3 = new Movie
            {
                Title = "Inception",
                Description = "A thief who steals corporate secrets through dream-sharing technology is given the task of planting an idea.",
                DurationMinutes = 148,
                Genre = "Sci-Fi",
                Language = "English",
                PosterUrl = "https://image.tmdb.org/t/p/w500/edv5CZvWj09upOsy2Y6IwDhK8bt.jpg",
                ReleaseDate = new DateTime(2010, 7, 16)
            };

            context.Movies.AddRange(movie1, movie2, movie3);
            await context.SaveChangesAsync();

            // Seed Showtimes (future dates)
            var baseDate = DateTime.UtcNow.Date.AddDays(1);
            var showtimes = new[]
            {
                new Showtime { MovieId = movie1.Id, TheaterHallId = hall1.Id, StartTimeUtc = baseDate.AddHours(10), BasePrice = 200m },
                new Showtime { MovieId = movie1.Id, TheaterHallId = hall1.Id, StartTimeUtc = baseDate.AddHours(14), BasePrice = 250m },
                new Showtime { MovieId = movie1.Id, TheaterHallId = hall2.Id, StartTimeUtc = baseDate.AddHours(18), BasePrice = 300m },
                new Showtime { MovieId = movie2.Id, TheaterHallId = hall1.Id, StartTimeUtc = baseDate.AddHours(11), BasePrice = 200m },
                new Showtime { MovieId = movie2.Id, TheaterHallId = hall2.Id, StartTimeUtc = baseDate.AddHours(15), BasePrice = 250m },
                new Showtime { MovieId = movie3.Id, TheaterHallId = hall1.Id, StartTimeUtc = baseDate.AddHours(19), BasePrice = 300m },
                new Showtime { MovieId = movie3.Id, TheaterHallId = hall2.Id, StartTimeUtc = baseDate.AddDays(1).AddHours(12), BasePrice = 200m },
            };
            context.Showtimes.AddRange(showtimes);
            await context.SaveChangesAsync();
        }
    }
}
