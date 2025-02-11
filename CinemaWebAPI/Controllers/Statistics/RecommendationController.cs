using BusinessLogic.Models.Recommendations;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/recommendations")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class RecommendationController : ControllerBase
    {
        private readonly IRecommendationService _recommendationService;

        public RecommendationController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(List<RecommendationDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetRecommendations(string userId)
        {
            var recommendations = await _recommendationService.GetRecommendationsAsync(userId);
            if (recommendations == null || recommendations.Count == 0)
            {
                return NotFound(new { Message = $"No recommendations found for user {userId}." });
            }
            return Ok(recommendations);
        }

        [HttpPost("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GenerateRecommendations(string userId)
        {
            try
            {
                await _recommendationService.GenerateRecommendationsAsync(userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
