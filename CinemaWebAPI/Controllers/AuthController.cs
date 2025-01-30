using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using DataAccessLayer.Data;
using DataAccess.Models.Users;
using BusinessLogicLayer.Interfaces;
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
        private readonly IMapper _mapper;

        public AuthController(UserManager<AppUser> userManager, ITokenGeneratorService tokenService, IAuthService authService, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var errorMessage = await _authService.RegisterUser(registerDTO);
            if (!string.IsNullOrEmpty(errorMessage))
                return BadRequest(errorMessage);

            var loginDTO = _mapper.Map<LoginDTO>(registerDTO);
            var loginResponse = await _authService.AuthenticateUserAsync(loginDTO);

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
