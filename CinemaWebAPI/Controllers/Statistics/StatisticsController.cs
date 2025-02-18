using BusinessLogic.Interfaces.Service.Statistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Statistics
{
    /// <summary>
    /// Controller for retrieving movie statistics.
    /// This controller provides endpoints for accessing various movie statistics such as booked tickets.
    /// </summary>
    [Route("api/statistics")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsController"/> class.
        /// </summary>
        /// <param name="statisticsService">The service responsible for retrieving statistics data.</param>
        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        /// <summary>
        /// Gets the number of booked tickets for a specific movie.
        /// </summary>
        /// <param name="movieId">The ID of the movie to retrieve statistics for.</param>
        /// <returns>The number of booked tickets for the specified movie.</returns>
        /// <response code="200">Returns the number of booked tickets.</response>
        /// <response code="404">Returns an error if the movie ID is not found.</response>
        [HttpGet("booked-tickets/{movieId}")]
        public async Task<IActionResult> GetBookedTicketsCount(int movieId)
        {
            int count = await _statisticsService.GetBookedTicketsCountAsync(movieId);

            // Check if the count is zero, or handle it as needed, e.g., return NotFound
            if (count == 0)
            {
                return NotFound(new { Message = $"No booked tickets found for movie with ID {movieId}." });
            }

            return Ok(count);
        }
    }
}
