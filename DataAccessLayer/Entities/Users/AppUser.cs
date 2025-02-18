using DataAccess.Models.Tickets;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Models.Users
{
    public class AppUser : IdentityUser
    {
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<UserPreference> Preferences { get; set; } = new List<UserPreference>();
    }
}