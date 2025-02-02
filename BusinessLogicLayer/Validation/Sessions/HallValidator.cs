using BusinessLogic.Models.Sessions;
using FluentValidation;

namespace BusinessLogicLayer.Validations 
{
    public class HallValidator : AbstractValidator<CreateHallDTO> 
    {
        public HallValidator()
        {
            RuleFor(h => h.Name)
                .NotEmpty().WithMessage("Hall name is required")
                .MaximumLength(100).WithMessage("Hall name must be at most 100 characters");            
            
            RuleFor(h => h.Capacity).NotEmpty().WithMessage("Hall capacity is required");
        }
    }
}