using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccess.Models.Users;
using DataAccessLayer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer
{
    public static class ServiceExtension
    {
        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<AppDbContext>();
        }
        public static void AddCinemaServices(this IServiceCollection services) 
        {
            services.AddScoped<IMovieService, MovieService>();
        }
    }
}
