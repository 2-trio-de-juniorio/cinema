using BusinessLogic.Models.Users;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing user preferences.
    /// </summary>
    [Route("api/userpreferences")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class UserPreferenceController : ControllerBase
    {
        private readonly IUserPreferenceService _preferenceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserPreferenceController"/> class.
        /// </summary>
        /// <param name="preferenceService">The service for managing user preferences.</param>
        public UserPreferenceController(IUserPreferenceService preferenceService)
        {
            _preferenceService = preferenceService;
        }

        /// <summary>
        /// Retrieves all preferences for a given user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of user preferences.</returns>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(List<UserPreferenceDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPreferences(string userId)
        {
            var preferences = await _preferenceService.GetUserPreferencesAsync(userId);
            if (preferences == null || preferences.Count == 0)
            {
                return NotFound(new { Message = $"No preferences found for user {userId}." });
            }
            return Ok(preferences);
        }

        /// <summary>
        /// Retrieves a specific user preference for a given user and movie.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="movieId">The ID of the movie.</param>
        /// <returns>The user preference if found, otherwise 404.</returns>
        [HttpGet("{userId}/{movieId}")]
        [ProducesResponseType(typeof(UserPreferenceDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPreference(string userId, int movieId)
        {
            var preference = await _preferenceService.GetUserPreferenceAsync(userId, movieId);
            if (preference == null)
            {
                return NotFound(new { Message = $"Preference for movie {movieId} for user {userId} not found." });
            }
            return Ok(preference);
        }

        /// <summary>
        /// Creates a new user preference.
        /// </summary>
        /// <param name="preferenceDto">The user preference data.</param>
        /// <returns>The created user preference.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(UserPreferenceDTO), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddPreference([FromBody] UserPreferenceDTO preferenceDto)
        {
            if (preferenceDto == null)
            {
                return BadRequest("Invalid preference data.");
            }
            await _preferenceService.AddUserPreferenceAsync(preferenceDto);
            return CreatedAtAction(nameof(GetPreference), new { userId = preferenceDto.UserId, movieId = preferenceDto.MovieId }, preferenceDto);
        }

        /// <summary>
        /// Updates an existing user preference.
        /// </summary>
        /// <param name="preferenceDto">The updated user preference data.</param>
        /// <returns>No content if the update is successful, or 404 if not found.</returns>
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdatePreference([FromBody] UserPreferenceDTO preferenceDto)
        {
            if (preferenceDto == null)
            {
                return BadRequest("Invalid preference data.");
            }
            try
            {
                await _preferenceService.UpdateUserPreferenceAsync(preferenceDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}
