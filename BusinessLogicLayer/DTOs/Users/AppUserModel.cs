using BusinessLogic.Models.Tickets;

namespace BusinessLogic.Models.Users
{
    public class AppUserModel
    {
        public List<TicketDTO> Tickets { get; set; } = new List<TicketDTO>();
    }
}
