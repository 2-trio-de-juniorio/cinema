using BusinessLogic.Models.Sessions;
using FluentValidation;

namespace BusinessLogicLayer.Validations 
{
    public class SeatValidator : AbstractValidator<CreateSeatDTO> 
    {
        public SeatValidator()
        {
            RuleFor(s => s.RowNumber).NotEmpty().WithMessage("Row number of a seat is required");
            RuleFor(s => s.SeatNumber).NotEmpty().WithMessage("Seat number is required");
        }
    }
}