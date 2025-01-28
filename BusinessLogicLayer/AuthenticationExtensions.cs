using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;

namespace BusinessLogicLayer
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAuthenticationDependencies(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddJwtAuthentication(configuration).AddAuthenticationServices();
            return services;
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

        private static void AddAuthenticationServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}