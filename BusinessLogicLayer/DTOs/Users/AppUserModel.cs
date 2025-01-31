using BusinessLogic.Models.Tickets;

namespace BusinessLogic.Models.Users
{
    public class AppUserModel
    {
        public List<TicketModel> Tickets { get; set; } = new List<TicketModel>();
    }
}
