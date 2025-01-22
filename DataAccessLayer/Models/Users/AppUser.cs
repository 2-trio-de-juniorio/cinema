using Microsoft.AspNetCore.Identity;
using System.Net.Sockets;

namespace DataAccessLayer.Models;

public class AppUser : IdentityUser
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<Ticket> Tickets { get; set; }
}