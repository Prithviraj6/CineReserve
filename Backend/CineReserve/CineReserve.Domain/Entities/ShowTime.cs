using CineReserve.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineReserve.Domain.Entities
{
    public class Showtime : BaseEntity
    {
        public int MovieId { get; set; }

        public int TheaterHallId { get; set; }

        public DateTime StartTimeUtc { get; set; }

        public decimal BasePrice { get; set; }

        public Movie Movie { get; set; } = null!;

        public TheaterHall TheaterHall { get; set; } = null!;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        public ICollection<TicketDetail> TicketDetails { get; set; } = new List<TicketDetail>();
    }
}
