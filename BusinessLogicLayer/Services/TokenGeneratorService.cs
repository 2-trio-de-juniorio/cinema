using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly IConfiguration _configuration;

        public TokenGeneratorService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(string userId, string role)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]);
            var tokenExpiration = int.Parse(_configuration["JWT:TokenExpirationMinutes"]);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = CreateTokenDescriptor(userId, role, key);

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        private SecurityTokenDescriptor CreateTokenDescriptor(string userId, string role, byte[] signingKey)
        {
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Role, role)
        }),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JWT:TokenExpirationMinutes"])),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(signingKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
        }
    }
}
