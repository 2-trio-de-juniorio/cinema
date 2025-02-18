using BusinessLogic.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserPreferenceService
    {
        Task<List<UserPreferenceDTO>> GetUserPreferencesAsync(string userId);
        Task<UserPreferenceDTO?> GetUserPreferenceAsync(string userId, int movieId);
        Task AddUserPreferenceAsync(UserPreferenceDTO preferenceDto);
        Task UpdateUserPreferenceAsync(UserPreferenceDTO preferenceDto);
    }
}
