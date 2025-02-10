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
<<<<<<< HEAD
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

        /// <summary>
        /// Logout user
        /// </summary>
        /// <param name="refreshToken">Refresh token to be revoked</param>
        /// <returns>Success message</returns>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string refreshToken)
        {
            try
            {
                await _authService.LogoutAsync(refreshToken);
                return Ok("Logged out successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update user information
        /// </summary>
        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO updateUserDTO)
        {
            try
            {
                var response = await _authService.UpdateUserAsync(User.Identity.Name, updateUserDTO);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
=======
>>>>>>> 92859c95b39db5718fb00d9a55701be212934908
        }

        /// <summary>
        /// Delete user account
        /// </summary>
        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser()
        {
<<<<<<< HEAD
            try
            {
                await _authService.DeleteUserAsync(User.Identity.Name);
                return Ok("Account successfully deleted.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
=======
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var response = await _authService.AuthenticateUserAsync(loginDTO);
            return Ok(response);
>>>>>>> 92859c95b39db5718fb00d9a55701be212934908
        }

        /// <summary>
        /// Used to registration admin (only for users with "Admin" role)
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        [Authorize(Policy = UserRole.Admin)]
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDTO registerDTO)
        {
<<<<<<< HEAD
            try
            {
                var response = await _authService.RegisterUser(registerDTO, "Admin");
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
=======
            var tokens = await _authService.RefreshAccessTokenAsync(refreshToken);
            return Ok(tokens);
>>>>>>> 92859c95b39db5718fb00d9a55701be212934908
        }
    }
}
