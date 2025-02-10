using BusinessLogicLayer.DTOs;
using FluentValidation;

namespace BusinessLogicLayer.Validations 
{
    public class RegisterValidator : AbstractValidator<RegisterDTO> 
    {
        public RegisterValidator()
        {
            RuleFor(r => r.Username).NotEmpty().WithMessage("Username is required");
            RuleFor(r => r.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(r => r.Email).EmailAddress().WithMessage("Email address must be valid");
        }
    }
}