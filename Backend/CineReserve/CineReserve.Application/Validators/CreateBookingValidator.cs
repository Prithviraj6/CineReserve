using CineReserve.Application.DTOs.Booking;
using FluentValidation;

namespace CineReserve.Application.Validators
{
    public class CreateBookingValidator : AbstractValidator<CreateBookingDto>
    {
        public CreateBookingValidator()
        {
            RuleFor(x => x.ShowtimeId)
                .GreaterThan(0).WithMessage("Valid showtime is required");

            RuleFor(x => x.Seats)
                .NotEmpty().WithMessage("At least one seat must be selected")
                .Must(seats => seats.Count <= 10).WithMessage("Maximum 10 seats per booking");

            RuleForEach(x => x.Seats).ChildRules(seat =>
            {
                seat.RuleFor(s => s.SeatId)
                    .GreaterThan(0).WithMessage("Valid seat ID is required");
            });
        }
    }
}
