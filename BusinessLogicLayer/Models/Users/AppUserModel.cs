using BusinessLogic.Models.Tickets;

namespace BusinessLogic.Models.Users;
public class AppUserModel
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public List<TicketModel> Tickets { get; set; } = new List<TicketModel>();
}