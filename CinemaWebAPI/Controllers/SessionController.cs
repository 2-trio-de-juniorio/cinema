using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CinemaWebAPI.Services; 

namespace CinemaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        /// <summary>
        /// Set session value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        [HttpPost("set")]
        public IActionResult SetSession([FromQuery] string key, [FromQuery] string value)
        {
            _sessionService.SetSessionValue(key, value);
            return Ok(new { message = "Session value set" });
        }

        /// <summary>
        /// Get session value
        /// </summary>
        /// <param name="key"></param>
        [HttpGet("get")]
        public IActionResult GetSession([FromQuery] string key)
        {
            var value = _sessionService.GetSessionValue(key);
            if (value == null)
            {
                return NotFound(new { message = "Session value not found" });
            }

            return Ok(new { key, value });
        }

        /// <summary>
        /// Clear session
        /// </summary>
        [HttpPost("clear")]
        public IActionResult ClearSession()
        {
            _sessionService.ClearSession();
            return Ok(new { message = "Session cleared" });
        }

        /// <summary>
        /// update session 
        /// </summary>
        [HttpPost("keep-alive")]
        public IActionResult KeepSessionAlive()
        {
            _sessionService.SetSessionValue("LastActive", DateTime.UtcNow.ToString());
            return Ok(new { message = "Session updated" });
        }
    }
}
