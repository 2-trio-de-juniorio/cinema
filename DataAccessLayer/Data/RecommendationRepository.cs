// DataAccessLayer/Repositories/RecommendationRepository.cs
using DataAccess.Models.Recommendations;
using DataAccessLayer.Data;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class RecommendationRepository : Repository<Recommendation>, IRecommendationRepository
    {
        public RecommendationRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Recommendation>> GetRecommendationsByUserIdAsync(string userId)
        {
            return await _entities
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.Score)
                .ToListAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Recommendation> recommendations)
        {
            await _entities.AddRangeAsync(recommendations);
        }
    }
}
