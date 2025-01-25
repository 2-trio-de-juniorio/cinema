using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BusinessLogicLayer;
using DataAccessLayer.Data;
using DataAccess.Models.Users;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.DTOs;

namespace CinemaWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;
        private readonly AppDbContext _context;
        private readonly IAuthService _authService;

        public AuthController(UserManager<AppUser> userManager, TokenService tokenService, AppDbContext context)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _context = context;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await _userManager.FindByNameAsync(loginRequest.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
                return Unauthorized("Invalid username or password");

            var accessToken = _tokenService.GenerateAccessToken(user.Id, "User");
            var refreshToken = _tokenService.GenerateRefreshToken();

            await _authService.SaveRefreshTokenAsync(refreshToken, user.Id);

            var response = new LoginDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return Ok(response);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            try
            {
                var tokens = await _authService.RefreshAccessTokenAsync(refreshToken);
                return Ok(new
                {
                    AccessToken = tokens.AccessToken,
                    RefreshToken = tokens.RefreshToken
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
