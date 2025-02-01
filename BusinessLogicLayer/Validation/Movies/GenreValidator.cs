using DataAccess.Models.Movies;
using FluentValidation;

public class GenreValidator : AbstractValidator<Genre>
{
    public GenreValidator()
    {
        RuleFor(Genre => Genre.Name)
            .NotEmpty().WithMessage("Genre name is required")
            .Length(2, 50).WithMessage("Genres name must be between 2 and 50 characters");
    }
}
