using BusinessLogic.Models.Sessions;
using BusinessLogic.Models.Users;

namespace BusinessLogic.Models.Tickets
{
    public class TicketModel
    {
        public AppUserProfile? User { get; set; }
        public SessionModel? Session { get; set; }
        public SeatModel? Seat { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
