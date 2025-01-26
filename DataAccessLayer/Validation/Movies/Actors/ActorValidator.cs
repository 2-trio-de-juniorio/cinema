using DataAccess.Models.Movies.Actors;
using FluentValidation;

public class ActorValidator : AbstractValidator<Actor>
{
    public ActorValidator()
    {
        RuleFor(actor => actor.Firstname)
            .NotEmpty().WithMessage("Firstname is required")
            .Length(2, 50).WithMessage("Firstname must be between 2 and 50 characters");

        RuleFor(actor => actor.Lastname)
            .NotEmpty().WithMessage("Lastname is required")
            .Length(2, 50).WithMessage("Lastname must be between 2 and 50 characters");
    }
}
