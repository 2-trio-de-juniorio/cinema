using BusinessLogic.Models.Tickets;
using FluentValidation;

namespace BusinessLogicLayer.Validations 
{
    public class TicketValidator : AbstractValidator<CreateTicketDTO> 
    {
        public TicketValidator()
        {
            RuleFor(t => t.UserId).NotEmpty().WithMessage("Ticket's user id is required");

            RuleFor(t => t.BookingDate).Must(BeAValidDate).WithMessage("Booking date must be valid");
        }

        private bool BeAValidDate(DateTime bookingDate) 
        {
            return bookingDate < DateTime.Now;
        }
    }
}