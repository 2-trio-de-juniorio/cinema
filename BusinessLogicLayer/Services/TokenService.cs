using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    public class TokenService : ITokenService
    {
        private const string JwtSigningKeyPath = "JWT:SigningKey";
        private const int TokenExpirationMinutes = 15; 
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(string userId, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var signingKey = Encoding.UTF8.GetBytes(_configuration[JwtSigningKeyPath]);

            var tokenDescriptor = CreateTokenDescriptor(userId, role, signingKey);

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
                Expires = DateTime.UtcNow.AddMinutes(TokenExpirationMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(signingKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
        }
    }
}
