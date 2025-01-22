using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer;

public static class ServiceExtensions
{
    public static void AddDbContext(this IServiceCollection service, string connectionString)
    {
        service.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
}