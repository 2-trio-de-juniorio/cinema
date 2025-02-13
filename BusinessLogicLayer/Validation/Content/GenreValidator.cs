using BusinessLogic.Models.Movies;
using FluentValidation;
namespace BusinessLogicLayer.Validations
{
    public class GenreValidator : AbstractValidator<CreateGenreDTO>
    {
        public GenreValidator()
        {
            RuleFor(g => g.Name)
                .NotEmpty().WithMessage("Genre name is required")
                .Length(2, 50).WithMessage("Genres name must be between 2 and 50 characters");
        }
    }
}