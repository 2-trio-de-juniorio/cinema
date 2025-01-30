using BusinessLogic.Models.Sessions;

namespace BusinessLogicLayer.Interfaces
{
    public interface ISessionService 
    {
        Task<List<SessionDTO>> GetAllSessionsAsync();
        Task<SessionDTO?> GetSessionByIdAsync(int id);
        Task<int> CreateSessionAsync(SessionDTO sessionDto);
        Task<bool> UpdateSessionAsync(int id, SessionDTO sessionDto);
        Task RemoveSessionAsync(int id);
    }
}