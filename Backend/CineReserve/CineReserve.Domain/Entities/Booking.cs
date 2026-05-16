using CineReserve.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineReserve.Domain.Entities
{
    public class Booking : BaseEntity
    {
        public int UserId { get; set; }

        public int ShowtimeId { get; set; }

        public string BookingReference { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        public DateTime BookedAtUtc { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;

        public Showtime Showtime { get; set; } = null!;

        public ICollection<TicketDetail> TicketDetails { get; set; } = new List<TicketDetail>();
    }
}
