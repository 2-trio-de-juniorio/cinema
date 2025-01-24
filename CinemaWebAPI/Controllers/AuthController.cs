using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BusinessLogicLayer;
using DataAccessLayer.Data;
using DataAccess.Models.Users;

namespace CinemaWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;
        private readonly AppDbContext _context;

        public AuthController(UserManager<AppUser> userManager, TokenService tokenService, AppDbContext context)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
                return Unauthorized("Invalid username or password");

            var accessToken = _tokenService.GenerateAccessToken(user.Id, "User");
            var refreshToken = _tokenService.GenerateRefreshToken();


            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                Expires = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };
            _context.RefreshTokens.Add(refreshTokenEntity);
            await _context.SaveChangesAsync();

            return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(string refreshToken)
        {
            var tokenEntity = _context.RefreshTokens.SingleOrDefault(t => t.Token == refreshToken && !t.IsRevoked);

            if (tokenEntity == null || tokenEntity.Expires < DateTime.UtcNow)
                return Unauthorized("Invalid or expired refresh token");

            var user = await _userManager.FindByIdAsync(tokenEntity.UserId);
            if (user == null)
                return Unauthorized("User not found");

  
            var newAccessToken = _tokenService.GenerateAccessToken(user.Id, "User");

            return Ok(new { AccessToken = newAccessToken });
        }
    }
}
