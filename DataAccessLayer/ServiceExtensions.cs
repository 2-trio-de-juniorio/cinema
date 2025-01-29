using DataAccessLayer.Data;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer;

public static class ServiceExtensions
{
    public static IServiceCollection AddDataAccessDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.AddDbContext(configuration.GetConnectionString("DefaultConnection")).AddRepository();
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
    {
        return services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
    public static void AddUnitOfWork(this IServiceCollection services) 
    {
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
    }
}