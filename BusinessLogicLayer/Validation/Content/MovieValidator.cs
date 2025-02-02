using BusinessLogic.Models.Movies;
using FluentValidation;
using FluentValidation.Validators;

namespace BusinessLogicLayer.Validations
{
    public class MovieValidator : AbstractValidator<CreateMovieDTO>
    {
        public MovieValidator()
        {
            RuleFor(m => m.Title)
                .NotEmpty().WithMessage("Movie title is required")
                .Length(2, 100).WithMessage("Movie title must be between 2 and 100 characters");

            RuleFor(m => m.Description).NotEmpty().WithMessage("Movie description is required");

            RuleFor(m => m.Duration).NotEmpty().WithMessage("Movie duration is required");

            RuleFor(m => m.ReleaseDate).NotEmpty().WithMessage("Movie release date is required");

            RuleFor(m => m.TrailerUrl)
                .NotEmpty().WithMessage("Movie trailer url is required")
                .MaximumLength(2083).WithMessage("Movie trailer url must be maximum 2083 characters");

            RuleFor(m => m.PosterUrl)
                .NotEmpty().WithMessage("Movie poster url is required")
                .MaximumLength(2083).WithMessage("Movie poster url must be maximum 2083 characters");

            RuleFor(m => (decimal)m.Rating)
                .PrecisionScale(3, 1, ignoreTrailingZeros: true)
                .WithMessage("Movie rating must be a number with at most 3 digits and 1 decimal place.");

        }
    }
}