using BusinessLogicLayer.DTOs;
using FluentValidation;
using static BusinessLogicLayer.Validations.PasswordValidationHelper;

namespace BusinessLogicLayer.Validations 
{
    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(l => l.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be minimum 8 digits")
                .Must(ContainDigit).WithMessage("Password must contain a digit")
                .Must(ContainLowercase).WithMessage("Password must contain at least one lowercase letter")
                .Must(ContainUppercase).WithMessage("Password must contain at least one uppercase letter")
                .Must(ContainNonAlphanumeric).WithMessage("Password must contain at least one alphanumeric characher");

            RuleFor(l => l.Username).NotEmpty().WithMessage("Username is required");
        }
    }
}