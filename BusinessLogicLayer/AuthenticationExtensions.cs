using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Models;

namespace BusinessLogicLayer
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAuthenticationDependencies(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddJwtAuthentication(configuration)
                .AddAuthenticationServices()
                .AddAuthorizationAccess();
        }

        private static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    options.DefaultChallengeScheme =
                        options.DefaultForbidScheme =
                            options.DefaultScheme =
                                options.DefaultSignInScheme =
                                    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JWT:SigningKey"] ?? string.Empty))
                };
            });

            return services;
        }

        private static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
        {
            return services
                .AddScoped<ITokenGeneratorService, TokenGeneratorService>()
                .AddScoped<IAuthService, AuthService>();
        }

        private static IServiceCollection AddAuthorizationAccess(this IServiceCollection services)
        {
            return services
                .AddAuthorization(options =>
                {
                    options.AddPolicy(UserRole.Admin, policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireRole(UserRole.Admin);
                    });
                });
        }
    }
}