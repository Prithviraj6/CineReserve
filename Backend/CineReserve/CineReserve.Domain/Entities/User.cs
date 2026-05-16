using CineReserve.Domain.Common;
using CineReserve.Domain.Enums;

namespace CineReserve.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public decimal CreditBalance { get; set; }

        public UserRole Role { get; set; } = UserRole.User;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
