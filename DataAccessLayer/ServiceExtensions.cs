using DataAccessLayer.Data;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDataAccessDependencies(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddDbContext(configuration.GetConnectionString("DefaultConnection"))
                .AddUnitOfWork();
        }

        private static IServiceCollection AddDbContext(this IServiceCollection services, string? connectionString)
        {
            return services.AddDbContext<AppDbContext>(options =>
                //options.UseSqlServer(connectionString));
                options.UseInMemoryDatabase("TestDb"));
        }

        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }
    }
}