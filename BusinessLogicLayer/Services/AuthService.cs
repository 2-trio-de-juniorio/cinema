using System;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models.Users;
using DataAccessLayer.Data;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<AppUser> userManager, AppDbContext context, ITokenService tokenService)
        {
            _userManager = userManager;
            _context = context;
            _tokenService = tokenService;
        }

        public async Task SaveRefreshTokenAsync(string refreshToken, string userId)
        {
            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                UserId = userId,
                Expires = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            _context.RefreshTokens.Add(refreshTokenEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsUserValid(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return false;

            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<(string AccessToken, string RefreshToken)> RefreshAccessTokenAsync(string refreshToken)
        {
            var tokenEntity = _context.RefreshTokens
                .SingleOrDefault(t => t.Token == refreshToken && !t.IsRevoked);

            if (tokenEntity == null || tokenEntity.Expires < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Invalid or expired refresh token");

            var user = await _userManager.FindByIdAsync(tokenEntity.UserId);
            if (user == null)
                throw new UnauthorizedAccessException("User not found");

            var newAccessToken = _tokenService.GenerateAccessToken(user.Id, "User");

            var newRefreshToken = _tokenService.GenerateRefreshToken();

            tokenEntity.Token = newRefreshToken;
            tokenEntity.Expires = DateTime.UtcNow.AddDays(7);
            tokenEntity.IsRevoked = false;

            _context.RefreshTokens.Update(tokenEntity);
            await _context.SaveChangesAsync();

            return (newAccessToken, newRefreshToken);
        }
    }
}
