using BusinessLogic.Models.Movies;
using FluentValidation;
namespace BusinessLogicLayer.Validations
{
    public class ActorValidator : AbstractValidator<CreateActorDTO>
    {
        public ActorValidator()
        {
            RuleFor(a => a.Firstname)
                .NotEmpty().WithMessage("Firstname is required")
                .Length(2, 50).WithMessage("Firstname must be between 2 and 50 characters");

            RuleFor(a => a.Lastname)
                .NotEmpty().WithMessage("Lastname is required")
                .Length(2, 50).WithMessage("Lastname must be between 2 and 50 characters");
        }
    }

}