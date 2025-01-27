using System;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models.Users;
using DataAccessLayer.Data;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly ITokenGeneratorService _tokenService;

        public AuthService(UserManager<AppUser> userManager, AppDbContext context, ITokenGeneratorService tokenService)
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

        public async Task<string> RegisterUser(RegisterDTO registerDTO)
        {
            var user = new AppUser
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Email
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return $"Registration failed: {errors}";
            }

            return null;
        }

        public async Task<bool> IsUserValid(LoginDTO userCredentials)
        {
            var user = await _userManager.FindByNameAsync(userCredentials.Username);
            if (user == null)
                return false;

            return await _userManager.CheckPasswordAsync(user, userCredentials.Password);
        }

        public async Task<LoginResponseDTO> RefreshAccessTokenAsync(string refreshToken)
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

            return new LoginResponseDTO
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
