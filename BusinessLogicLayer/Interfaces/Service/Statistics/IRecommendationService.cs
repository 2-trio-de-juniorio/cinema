using BusinessLogic.Models.Recommendations;

namespace BusinessLogicLayer.Interfaces
{
    public interface IRecommendationService
    {
        Task<List<RecommendationDTO>> GetRecommendationsAsync(string userId);
        Task GenerateRecommendationsAsync(string userId);
    }
}
