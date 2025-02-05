using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.DTOs;
using Microsoft.AspNetCore.Authorization;
using DataAccessLayer.Models;

namespace CinemaWebAPI.Controllers
{
    /// <summary>
    /// Used to provide user auth, like registration, login, refresh token
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <inheritdoc />
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Used to registration common user
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _authService.RegisterUser(registerDTO);
            return Ok(response);
        }

        /// <summary>
        /// Used to log in as common user
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var response = await _authService.AuthenticateUserAsync(loginDTO);
            return Ok(response);
        }

        /// <summary>
        /// Used to refresh token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var tokens = await _authService.RefreshAccessTokenAsync(refreshToken);
            return Ok(tokens);
        }
    }
}