using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CreateGenreDTO = BusinessLogic.Models.Movies.CreateGenreDTO;

namespace CinemaWebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _actorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenresController"/> class.
        /// </summary>
        /// <param name="actorService">The service responsible for actor-related business logic.</param>
        public GenresController(IGenreService actorService)
        {
            _actorService = actorService;
        }

        /// <summary>
        /// Retrieves a list of all actors.
        /// </summary>
        /// <returns>A list of <see cref="BusinessLogic.Models.Movies.GenreDTO"/> objects representing all actors.</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllGenres()
        {
            List<BusinessLogic.Models.Movies.GenreDTO> actors = await _actorService.GetAllGenresAsync();

            return Ok(actors);
        }

        /// <summary>
        /// Retrieves details of a actor by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the actor.</param>
        /// <returns>A <see cref="BusinessLogic.Models.Movies.GenreDTO"/> object representing the actor, or HTTP 404 if not found.</returns>
        [HttpGet("{id}", Name = "GetGenreById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetGenreById([FromRoute] int id)
        {
            BusinessLogic.Models.Movies.GenreDTO? actor = await _actorService.GetGenreByIdAsync(id);

            if (actor == null)
            {
                return NotFound(new { Message = $"Genre with ID {id} not found." });
            }

            return Ok(actor);
        }

        /// <summary>
        /// Creates a new actor.
        /// </summary>
        /// <param name="GenreDTO">A <see cref="GenreDTO"/> object containing the details of the actor to create.</param>
        /// <returns>An HTTP 201 response if the actor is created successfully.</returns>
        [HttpPost]
        //[Authorize(Policy = UserRole.Admin)]
        public async Task<IActionResult> CreateGenreAsync([FromBody] CreateGenreDTO GenreDTO)
        {
            int id = await _actorService.CreateGenreAsync(GenreDTO);
            return CreatedAtRoute(nameof(GetGenreById), new { id }, GenreDTO);
        }

        /// <summary>
        /// Updates the details of an existing actor.
        /// </summary>
        /// <param name="id">The unique identifier of the genre to update.</param>
        /// <param name="createGenreDto"></param>
        /// <returns>
        /// An HTTP 204 response if the <paramref name="id"/> was found and HTTP 404 otherwise.
        /// </returns>
        [HttpPut("{id}")]
        //[Authorize(Policy = UserRole.Admin)]
        public async Task<IActionResult> UpdateGenreAsync([FromRoute] int id, [FromBody] CreateGenreDTO createGenreDto)
        {
            if (!await _actorService.UpdateGenreAsync(id, createGenreDto))
            {
                return NotFound(new { Message = $"Genre with ID {id} not found." });
            }

            return CreatedAtRoute(nameof(GetGenreById), new { id }, createGenreDto);
        }

        /// <summary>
        /// Deletes an actor by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the actor to delete.</param>
        /// <returns>An HTTP 204 response if deleted, or 404 if not found.</returns>
        [HttpDelete("{id}")]
        //[Authorize(Policy = UserRole.Admin)]
        public async Task<IActionResult> DeleteGenreAsync([FromRoute] int id)
        {
            bool successfully = await _actorService.RemoveGenreAsync(id);

            if (!successfully)
            {
                return NotFound(new { Message = $"Genre with ID {id} not found." });
            }

            return NoContent();
        }
    }
}