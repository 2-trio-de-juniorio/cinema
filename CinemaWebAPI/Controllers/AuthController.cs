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

            var loginResponse = await _authService.AuthenticateUserAsync(new LoginDTO
            {
                Username = registerDTO.Username,
                Password = registerDTO.Password
            });

            return Ok(loginResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var response = await _authService.AuthenticateUserAsync(loginDTO);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
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
