using DataAccess.Models.Tickets;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Models.Users
{
    public class AppUser : IdentityUser
    {
        public ICollection<Ticket> Tickets { get; set; }
    }
}