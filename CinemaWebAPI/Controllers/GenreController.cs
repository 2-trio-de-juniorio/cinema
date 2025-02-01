using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Models.Movies;

namespace CinemaWebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenreController"/> class.
        /// </summary>
        /// <param name="genreService">The service responsible for genre-related business logic.</param>
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        /// <summary>
        /// Retrieves a list of all genres.
        /// </summary>
        /// <returns>A list of <see cref="GenreDTO"/> objects representing all genres.</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllGenres()
        {
            List<GenreDTO> genres = await _genreService.GetAllGenresAsync();

            return Ok(genres);
        }

        /// <summary>
        /// Retrieves details of a genre by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the genre.</param>
        /// <returns>A <see cref="GenreDTO"/> object representing the genre, or HTTP 404 if not found.</returns>
        [HttpGet("{id}", Name = "GetGenreById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetGenreById([FromRoute] int id)
        {
            GenreDTO? genre = await _genreService.GetGenreByIdAsync(id);

            if (genre == null)
            {
                return NotFound(new { Message = $"Genre with ID {id} not found." });
            }

            return Ok(genre);
        }

        /// <summary>
        /// Creates a new genre.
        /// </summary>
        /// <param name="createGenreDTO">A <see cref="CreateGenreDTO"/> object containing the details of the genre to create.</param>
        /// <returns>An HTTP 201 response if the genre is created successfully.</returns>
        [HttpPost]
        //[Authorize(Policy = UserRole.Admin)]
        public async Task<IActionResult> CreateGenreAsync([FromBody] CreateGenreDTO createGenreDTO)
        {
            int id = await _genreService.CreateGenreAsync(createGenreDTO);
            return CreatedAtRoute(nameof(GetGenreById), new { id }, createGenreDTO);
        }

        /// <summary>
        /// Updates the details of an existing genre.
        /// </summary>
        /// <param name="id">The unique identifier of the genre to update.</param>
        /// <param name="createGenreDTO"></param>
        /// <returns>
        /// An HTTP 204 response if the <paramref name="id"/> was found and HTTP 404 otherwise.
        /// </returns>
        [HttpPut("{id}")]
        //[Authorize(Policy = UserRole.Admin)]
        public async Task<IActionResult> UpdateGenreAsync([FromRoute] int id, [FromBody] CreateGenreDTO createGenreDTO)
        {
            if (!await _genreService.UpdateGenreAsync(id, createGenreDTO))
            {
                return NotFound(new { Message = $"Genre with ID {id} not found." });
            }

            return CreatedAtRoute(nameof(GetGenreById), new { id }, createGenreDTO);
        }

        /// <summary>
        /// Deletes an genre by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the genre to delete.</param>
        /// <returns>An HTTP 204 response if deleted, or 404 if not found.</returns>
        [HttpDelete("{id}")]
        //[Authorize(Policy = UserRole.Admin)]
        public async Task<IActionResult> DeleteGenreAsync([FromRoute] int id)
        {
            bool successfully = await _genreService.RemoveGenreAsync(id);

            if (!successfully)
            {
                return NotFound(new { Message = $"Genre with ID {id} not found." });
            }

            return NoContent();
        }
    }
}