using BusinessLogicLayer.DTOs;
using FluentValidation;

namespace BusinessLogicLayer.Validations 
{
    public class LoginValidator : AbstractValidator<LoginDTO> 
    {
        public LoginValidator()
        {
            RuleFor(r => r.Username).NotEmpty().WithMessage("Username is required");
            RuleFor(r => r.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}