using DataAccess.Models.Users;

namespace DataAccessLayer.Interfaces
{
    public interface IUserPreferenceRepository : IRepository<UserPreference>
    {
        Task<List<UserPreference>> GetPreferencesByUserIdAsync(string userId);
        Task<UserPreference?> GetPreferenceAsync(string userId, int movieId);
    }
}
