using DataAccess.Models.Users;
using DataAccessLayer.Data;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class UserPreferenceRepository : Repository<UserPreference>, IUserPreferenceRepository
    {
        public UserPreferenceRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<UserPreference>> GetPreferencesByUserIdAsync(string userId)
        {
            return await _entities
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<UserPreference?> GetPreferenceAsync(string userId, int movieId)
        {
            return await _entities
                .FirstOrDefaultAsync(p => p.UserId == userId && p.MovieId == movieId);
        }
    }
}
