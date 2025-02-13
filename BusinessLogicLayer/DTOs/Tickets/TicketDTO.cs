using BusinessLogic.Models.Sessions;

namespace BusinessLogic.Models.Tickets
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public SessionDTO Session { get; set; } = null!;
        public SeatDTO Seat { get; set; } = null!;
        public DateTime BookingDate { get; set; } 
    }
}
