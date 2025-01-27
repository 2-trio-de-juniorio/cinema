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
        private readonly ITokenGeneratorService _tokenService;
        private readonly AppDbContext _context;
        private readonly IAuthService _authService;

        public AuthController(UserManager<AppUser> userManager, ITokenGeneratorService tokenService, IAuthService authService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var errorMessage = await _authService.RegisterUser(registerDTO);
            if (!string.IsNullOrEmpty(errorMessage))
                return BadRequest(errorMessage);

            return Ok("Registration successful");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _userManager.FindByNameAsync(loginDTO.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password))
                return Unauthorized("Invalid username or password");

            var accessToken = _tokenService.GenerateAccessToken(user.Id, "User");
            var refreshToken = _tokenService.GenerateRefreshToken();

            await _authService.SaveRefreshTokenAsync(refreshToken, user.Id);

            var response = new LoginResponseDTO
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
                return Ok(tokens);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
