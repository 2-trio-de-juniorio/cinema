using BusinessLogic.Models.Sessions;
using FluentValidation;

namespace BusinessLogicLayer.Validations 
{
    public class SessionValidator : AbstractValidator<CreateSessionDTO> 
    {
        public SessionValidator()
        {
            
            RuleFor(s => s.StartTime).NotEmpty().WithMessage("Start time of a session is required");

            RuleFor(s => (decimal)s.Price)
                .PrecisionScale(10, 2, ignoreTrailingZeros: true)
                .WithMessage("Movie rating must be a number with at most 10 digits and 2 decimal place.");

        }
    }
}