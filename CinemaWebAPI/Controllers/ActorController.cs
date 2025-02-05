using BusinessLogic.Models.Movies;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Models;

namespace CinemaWebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;
        /// <summary>
        /// Initializes a new instance of the <see cref="ActorController"/> class.
        /// </summary>
        /// <param name="actorService">The service responsible for actor-related business logic.</param>
        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }

        /// <summary>
        /// Retrieves a list of all actors.
        /// </summary>
        /// <returns>A list of <see cref="ActorDTO"/> objects representing all actors.</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllActors()
        {
            List<ActorDTO> actors = await _actorService.GetAllActorsAsync();
            return Ok(actors);
        }

        /// <summary>
        /// Retrieves details of a actor by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the actor.</param>
        /// <returns>A <see cref="ActorDTO"/> object representing the actor, or HTTP 404 if not found.</returns>
        [HttpGet("{id}", Name = "GetActorById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetActorById([FromRoute] int id)
        {
            ActorDTO? actorDTO = await _actorService.GetActorByIdAsync(id);

            if (actorDTO == null)
            {
                throw new KeyNotFoundException($"Actor with ID {id} not found.");
            }

            return Ok(actorDTO);
        }

        /// <summary>
        /// Creates a new actor.
        /// </summary>
        /// <param name="createActorDTO">A <see cref="CreateActorDTO"/> object containing the details of the actor to create.</param>
        /// <returns>An HTTP 201 response if the actor is created successfully.</returns>
        [HttpPost]
        //[Authorize(Policy = UserRole.Admin)]
        public async Task<IActionResult> CreateActorAsync([FromBody] CreateActorDTO createActorDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int id = await _actorService.CreateActorAsync(createActorDTO);
            return CreatedAtRoute(nameof(GetActorById), new { id }, createActorDTO);
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            if (!await _actorService.UpdateActorAsync(id, createActorDto))
            {
                throw new KeyNotFoundException($"Actor with ID {id} not found.");
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
                throw new KeyNotFoundException($"Actor with ID {id} not found.");
            }

            return NoContent();
        }
    }
}