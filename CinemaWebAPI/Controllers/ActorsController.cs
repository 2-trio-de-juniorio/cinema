using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CreateActorDTO = BusinessLogic.Models.Movies.CreateActorDTO;

namespace CinemaWebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ActorsController : ControllerBase
    {
        private readonly IActorService _actorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorsController"/> class.
        /// </summary>
        /// <param name="actorService">The service responsible for actor-related business logic.</param>
        public ActorsController(IActorService actorService)
        {
            _actorService = actorService;
        }

        /// <summary>
        /// Retrieves a list of all actors.
        /// </summary>
        /// <returns>A list of <see cref="BusinessLogic.Models.Movies.ActorDTO"/> objects representing all actors.</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllActors()
        {
            List<BusinessLogic.Models.Movies.ActorDTO> actors = await _actorService.GetAllActorsAsync();

            return Ok(actors);
        }

        /// <summary>
        /// Retrieves details of a actor by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the actor.</param>
        /// <returns>A <see cref="BusinessLogic.Models.Movies.ActorDTO"/> object representing the actor, or HTTP 404 if not found.</returns>
        [HttpGet("{id}", Name = "GetActorById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetActorById([FromRoute] int id)
        {
            BusinessLogic.Models.Movies.ActorDTO? actor = await _actorService.GetActorByIdAsync(id);

            if (actor == null)
            {
                return NotFound(new { Message = $"Actor with ID {id} not found." });
            }

            return Ok(actor);
        }

        /// <summary>
        /// Creates a new actor.
        /// </summary>
        /// <param name="ActorDTO">A <see cref="ActorDTO"/> object containing the details of the actor to create.</param>
        /// <returns>An HTTP 201 response if the actor is created successfully.</returns>
        [HttpPost]
        //[Authorize(Policy = UserRole.Admin)]
        public async Task<IActionResult> CreateActorAsync([FromBody] CreateActorDTO ActorDTO)
        {
            int id = await _actorService.CreateActorAsync(ActorDTO);
            return CreatedAtRoute(nameof(GetActorById), new { id }, ActorDTO);
        }

        /// <summary>
        /// Updates the details of an existing actor.
        /// </summary>
        /// <param name="id">The unique identifier of the actor to update.</param>
        /// <param name="createActorDto"></param>
        /// <returns>
        /// An HTTP 204 response if the <paramref name="id"/> was found and HTTP 404 otherwise.
        /// </returns>
        [HttpPut("{id}")]
        //[Authorize(Policy = UserRole.Admin)]
        public async Task<IActionResult> UpdateActorAsync([FromRoute] int id, [FromBody] CreateActorDTO createActorDto)
        {
            if (!await _actorService.UpdateActorAsync(id, createActorDto))
            {
                return NotFound(new { Message = $"Actor with ID {id} not found." });
            }

            return CreatedAtRoute(nameof(GetActorById), new { id }, createActorDto);
        }

        /// <summary>
        /// Deletes an actor by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the actor to delete.</param>
        /// <returns>An HTTP 204 response if deleted, or 404 if not found.</returns>
        [HttpDelete("{id}")]
        //[Authorize(Policy = UserRole.Admin)]
        public async Task<IActionResult> DeleteActorAsync([FromRoute] int id)
        {
            bool successfully = await _actorService.RemoveActorAsync(id);

            if (!successfully)
            {
                return NotFound(new { Message = $"Actor with ID {id} not found." });
            }

            return NoContent();
        }
    }
}