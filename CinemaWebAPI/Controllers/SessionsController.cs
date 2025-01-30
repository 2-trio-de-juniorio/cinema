using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Models.Sessions;

namespace CinemaWebAPI.Controllers
{
    /// <summary>
    /// API controller responsible for managing session-related operations.
    /// This controller provides endpoints for CRUD operations for session records.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionsController"/> class.
        /// </summary>
        /// <param name="sessionService">The service responsible for session-related business logic.</param>
        public SessionsController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        /// <summary>
        /// Retrieves a list of all sessions.
        /// </summary>
        /// <returns>A list of <see cref="SessionDTO"/> objects representing all sessions.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllSessions()
        {
            List<SessionDTO> sessions = await _sessionService.GetAllSessionsAsync();

            return Ok(sessions);
        }

        /// <summary>
        /// Retrieves details of a session by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the session.</param>
        /// <returns>A <see cref="SessionDTO"/> object representing the session, or HTTP 404 if not found.</returns>
        [HttpGet("{id}", Name = "GetSessionById")]
        public async Task<IActionResult> GetSessionById([FromRoute] int id)
        {
            SessionDTO? session = await _sessionService.GetSessionByIdAsync(id);

            if (session == null)
            {
                return NotFound(new { Message = $"Session with ID {id} not found." });
            }

            return Ok(session);
        }

        /// <summary>
        /// Creates a new session.
        /// </summary>
        /// <param name="SessionDTO">A <see cref="SessionDTO"/> object containing the details of the session to create.</param>
        /// <returns>An HTTP 201 response if the session is created successfully.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateSessionAsync([FromBody] SessionDTO SessionDTO)
        {
            int id = await _sessionService.CreateSessionAsync(SessionDTO);
            return CreatedAtRoute(nameof(GetSessionById), new { id }, SessionDTO);
        }

        /// <summary>
        /// Updates the details of an existing session.
        /// </summary>
        /// <param name="id">The unique identifier of the session to update.</param>
        /// <param name="SessionDTO">A <see cref="SessionDTO"/> object containing the updated details of the session.</param>
        /// <returns>
        /// An HTTP 204 response if the <paramref name="id"/> was found and HTTP 404 otherwise.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSessionAsync([FromRoute] int id, [FromBody] SessionDTO SessionDTO)
        {
            if (!await _sessionService.UpdateSessionAsync(id, SessionDTO))
            {
                return NotFound(new { Message = $"Session with ID {id} not found." });
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a session by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the session to delete.</param>
        /// <returns>An HTTP 204 response if deleted, or 404 if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSessionAsync([FromRoute] int id)
        {
            var session = await _sessionService.GetSessionByIdAsync(id);
            if (session == null)
            {
                return NotFound(new { Message = $"Session with ID {id} not found." });
            }

            await _sessionService.RemoveSessionAsync(id);
            return NoContent();
        }
    }
}