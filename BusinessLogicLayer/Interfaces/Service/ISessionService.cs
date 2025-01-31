using BusinessLogic.Models.Movies;
using BusinessLogic.Models.Sessions;

namespace BusinessLogicLayer.Interfaces
{
    public interface ISessionService 
    {
        Task<List<SessionDTO>> GetAllSessionsAsync();
        Task<SessionDTO?> GetSessionByIdAsync(int id);
        Task<int> CreateSessionAsync(CreateSessionDTO createSessionDto);
        Task<bool> UpdateSessionAsync(int id, CreateSessionDTO createSessionDto);
        Task<bool> RemoveSessionAsync(int id);
        Task<List<SessionDTO>> GetFilteredSessionsAsync(SessionFilterDTO filter);

    }
}