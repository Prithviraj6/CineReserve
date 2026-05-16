using CineReserve.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineReserve.Domain.Entities
{
    public class TheaterHall : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public int TotalRows { get; set; }

        public int SeatsPerRow { get; set; }

        public ICollection<Seat> Seats { get; set; } = new List<Seat>();

        public ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
    }
}
