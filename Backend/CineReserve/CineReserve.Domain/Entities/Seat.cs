using CineReserve.Domain.Common;
using CineReserve.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineReserve.Domain.Entities
{
    public class Seat : BaseEntity
    {
        public int TheaterHallId { get; set; }

        public string RowLabel { get; set; } = string.Empty;

        public int SeatNumber { get; set; }

        public SeatType SeatType { get; set; }

        public TheaterHall TheaterHall { get; set; } = null!;

        public ICollection<TicketDetail> TicketDetails { get; set; } = new List<TicketDetail>();
    }
}
