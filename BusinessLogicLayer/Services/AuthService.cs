using AutoMapper;
using DataAccess.Models.Users;
using DataAccessLayer.Data;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
using BusinessLogicLayer.DTOs;
using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Services
{
    public class AuthService : IAuthService
    {
        private const int RefreshTokenExpiryDays = 7;

        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly ITokenGeneratorService _tokenService;
        private readonly IMapper _mapper;

        public AuthService(UserManager<AppUser> userManager, AppDbContext context, ITokenGeneratorService tokenService,
            IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task SaveRefreshTokenAsync(string refreshToken, string userId)
        {
            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                UserId = userId,
                Expires = DateTime.UtcNow.AddDays(RefreshTokenExpiryDays),
                IsRevoked = false
            };

            _context.RefreshTokens.Add(refreshTokenEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<LoginResponseDTO> RegisterUser(RegisterDTO registerDTO)
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
                throw new BadHttpRequestException($"Registration failed: {errors}");
            }
            
            IdentityResult rolesResult = await _userManager.AddToRoleAsync(user, "User");

            if (!rolesResult.Succeeded)
            {
                var errors = string.Join(", ", rolesResult.Errors.Select(e => e.Description));
                throw new BadHttpRequestException($"Registration failed: {errors}");
            }
            
            var loginDTO = _mapper.Map<LoginDTO>(registerDTO);
            
            return await AuthenticateUserAsync(loginDTO);
        }

        public async Task<bool> IsUserValid(LoginDTO userCredentials)
        {
            var user = await _userManager.FindByNameAsync(userCredentials.Username);

            if (user == null)
                return false;

            return await _userManager.CheckPasswordAsync(user, userCredentials.Password);
        }

        public async Task<LoginResponseDTO> AuthenticateUserAsync(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByNameAsync(loginDTO.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password))
                throw new UnauthorizedAccessException("Invalid username or password");

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Any())
                throw new UnauthorizedAccessException("User has no roles assigned");

            var role = roles.First();
            var accessToken = _tokenService.GenerateAccessToken(user.Id, role);
            var refreshToken = _tokenService.GenerateRefreshToken();

            await SaveRefreshTokenAsync(refreshToken, user.Id);

            return new LoginResponseDTO
            {
                Id = user.Id,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
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

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Any())
                throw new UnauthorizedAccessException("User has no roles assigned");

            var role = roles.First();
            var newAccessToken = _tokenService.GenerateAccessToken(user.Id, role);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            tokenEntity.Token = newRefreshToken;
            tokenEntity.Expires = DateTime.UtcNow.AddDays(RefreshTokenExpiryDays);
            tokenEntity.IsRevoked = false;

            _context.RefreshTokens.Update(tokenEntity);
            await _context.SaveChangesAsync();

            return new LoginResponseDTO
            {
                Id = user.Id,
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}