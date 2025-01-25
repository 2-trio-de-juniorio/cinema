using DataAccess.Configurations;
using DataAccess.Configurations.Sessions;
using DataAccess.Configurations.Tickets;
using DataAccess.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext() {}
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new AppUserConfiguration());
        builder.ApplyConfiguration(new TicketConfiguration());
        builder.ApplyConfiguration(new SessionConfiguration());
        builder.ApplyConfiguration(new HallConfiguration());
        builder.ApplyConfiguration(new SeatConfiguration());
        builder.ApplyConfiguration(new MovieConfiguration());
        builder.ApplyConfiguration(new GenreConfiguration());
        builder.ApplyConfiguration(new MovieGenreConfiguration());
        builder.ApplyConfiguration(new ActorConfiguration());
        builder.ApplyConfiguration(new MovieActorConfiguration());
    }

}