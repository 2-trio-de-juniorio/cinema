using BusinessLogicLayer.Dtos;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebAPI.Controllers;

/// <summary>
/// API controller responsible for managing movie-related operations.
/// This controller provides endpoints for CRUD operations for movie records.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase 
{
    private readonly IMovieService _movieService; 

    /// <summary>
    /// Initializes a new instance of the <see cref="MoviesController"/> class.
    /// </summary>
    /// <param name="movieService">The service responsible for movie-related business logic.</param>
    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    /// <summary>
    /// Retrieves a list of all movies.
    /// </summary>
    /// <returns>A list of <see cref="MovieDto"/> objects representing all movies.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllMovies() 
    {
        List<MovieDto> movies = await _movieService.GetAllMoviesAsync();

        return Ok(movies);
    }

    /// <summary>
    /// Retrieves details of a movie by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the movie.</param>
    /// <returns>A <see cref="MovieDto"/> object representing the movie, or HTTP 404 if not found.</returns>
    [HttpGet("{id}", Name = "GetMovieById")]
    public async Task<IActionResult> GetMovieById([FromRoute]int id) 
    {
        MovieDto? movie = await _movieService.GetMovieByIdAsync(id);

        if (movie == null) 
        {
            return NotFound(new {Message = $"Movie with ID {id} not found."});
        }

        return Ok(movie);
    }

    /// <summary>
    /// Creates a new movie.
    /// </summary>
    /// <param name="movieDto">A <see cref="MovieDto"/> object containing the details of the movie to create.</param>
    /// <returns>An HTTP 201 response if the movie is created successfully.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateMovieAsync([FromBody]MovieDto movieDto) 
    {
        int id = await _movieService.CreateMovieAsync(movieDto);
        return CreatedAtRoute(nameof(GetMovieById), new { id }, movieDto); 
    }

    /// <summary>
    /// Updates the details of an existing movie.
    /// </summary>
    /// <param name="id">The unique identifier of the movie to update.</param>
    /// <param name="movieDto">A <see cref="MovieDto"/> object containing the updated details of the movie.</param>
    /// <returns>
    /// An HTTP 204 response if the <paramref name="id"/> was found and HTTP 404 otherwise.
    /// </returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovieAsync([FromRoute]int id, [FromBody]MovieDto movieDto) 
    {
        if (!await _movieService.UpdateMovieAsync(id, movieDto)) 
        {
            return NotFound(new {Message = $"Movie with ID {id} not found."});
        }
        return NoContent();
    }

    /// <summary>
    /// Deletes a movie by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the movie to delete.</param>
    /// <returns>An HTTP 204 response if deleted, or 404 if not found.</returns>
    [HttpDelete("{id}")]
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
}