using DataAccess.Models.Recommendations;

namespace DataAccessLayer.Interfaces
{
    public interface IRecommendationRepository : IRepository<Recommendation>
    {
        Task<List<Recommendation>> GetRecommendationsByUserIdAsync(string userId);
        Task AddRangeAsync(IEnumerable<Recommendation> recommendations);
    }
}
