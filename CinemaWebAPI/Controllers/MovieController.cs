using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Models.Movies;


namespace CinemaWebAPI.Controllers
{
    /// <summary>
    /// API controller responsible for managing movie-related operations.
    /// This controller provides endpoints for CRUD operations for movie records.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieController"/> class.
        /// </summary>
        /// <param name="movieService">The service responsible for movie-related business logic.</param>
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Retrieves a list of all movies.
        /// </summary>
        /// <returns>A list of <see cref="MovieDTO"/> objects representing all movies.</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllMovies()
        {
            List<MovieDTO> movies = await _movieService.GetAllMoviesAsync();

            return Ok(movies);
        }

        /// <summary>
        /// Retrieves details of a movie by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the movie.</param>
        /// <returns>A <see cref="MovieDTO"/> object representing the movie, or HTTP 404 if not found.</returns>
        [HttpGet("{id}", Name = "GetMovieById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMovieById([FromRoute] int id)
        {
            MovieDTO? movie = await _movieService.GetMovieByIdAsync(id);

            if (movie == null)
            {
                return NotFound(new { Message = $"Movie with ID {id} not found." });
            }

            return Ok(movie);
        }

        /// <summary>
        /// Creates a new movie.
        /// </summary>
        /// <param name="movieDTO">A <see cref="MovieDTO"/> object containing the details of the movie to create.</param>
        /// <returns>An HTTP 201 response if the movie is created successfully.</returns>
        [HttpPost]
        //[Authorize(Policy = UserRole.Admin)]
        public async Task<IActionResult> CreateMovieAsync([FromBody] CreateMovieDTO movieDTO)
        {
            System.Console.WriteLine(User.IsInRole("Admin"));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            int id = await _movieService.CreateMovieAsync(movieDTO);
            return CreatedAtRoute(nameof(GetMovieById), new { id }, movieDTO);
        }

        /// <summary>
        /// Updates the details of an existing movie.
        /// </summary>
        /// <param name="id">The unique identifier of the movie to update.</param>
        /// <param name="movieDTO">A <see cref="MovieDTO"/> object containing the updated details of the movie.</param>
        /// <returns>
        /// An HTTP 204 response if the <paramref name="id"/> was found and HTTP 404 otherwise.
        /// </returns>
        [HttpPut("{id}")]
        //[Authorize(Policy = UserRole.Admin)]
        public async Task<IActionResult> UpdateMovieAsync([FromRoute] int id, [FromBody] CreateMovieDTO movieDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _movieService.UpdateMovieAsync(id, movieDTO))
            {
                return NotFound(new { Message = $"Movie with ID {id} not found." });
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a movie by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the movie to delete.</param>
        /// <returns>An HTTP 204 response if deleted, or 404 if not found.</returns>
        [HttpDelete("{id}")]
        //[Authorize(Policy = UserRole.Admin)]
        public async Task<IActionResult> DeleteMovieAsync([FromRoute] int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound(new { Message = $"Movie with ID {id} not found." });
            }

            await _movieService.RemoveMovieAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Get a list of movies with filtering and sorting.
        /// </summary>
        /// <param name="filter">Filters to search for movies.</param>
        /// <returns>List of movies that match filters.</returns>
        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFilteredMovies([FromQuery] MovieFilterDTO filter)
        {
            List<MovieDTO> movies = await _movieService.GetFilteredMoviesAsync(filter);

            if (!movies.Any())
            {
                return NotFound(new { Message = "No movies found for the specified filters." });
            }
            return Ok(movies);
        }

        

    }
}