using CineReserve.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineReserve.Domain.Entities
{
    public class TicketDetail : BaseEntity
    {
        public int BookingId { get; set; }

        public int ShowtimeId { get; set; }

        public int SeatId { get; set; }

        public decimal SeatPrice { get; set; }

        public Booking Booking { get; set; } = null!;

        public Showtime Showtime { get; set; } = null!;

        public Seat Seat { get; set; } = null!;
    }
}
