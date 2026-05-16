using CineReserve.Application.DTOs.Movie;
using FluentValidation;

namespace CineReserve.Application.Validators
{
    public class CreateMovieValidator : AbstractValidator<CreateMovieDto>
    {
        public CreateMovieValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Movie title is required")
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(2000);

            RuleFor(x => x.DurationMinutes)
                .GreaterThan(0).WithMessage("Duration must be positive")
                .LessThanOrEqualTo(600).WithMessage("Duration cannot exceed 600 minutes");

            RuleFor(x => x.Genre)
                .NotEmpty().WithMessage("Genre is required");

            RuleFor(x => x.Language)
                .NotEmpty().WithMessage("Language is required");
        }
    }
}
