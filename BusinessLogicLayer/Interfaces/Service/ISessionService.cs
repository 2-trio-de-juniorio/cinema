using BusinessLogicLayer.Dtos;

namespace BusinessLogicLayer.Interfaces
{
    public interface ISessionService 
    {
        Task<List<SessionDto>> GetAllSessionsAsync();
        Task<SessionDto?> GetSessionByIdAsync(int id);
        Task<int> CreateSessionAsync(SessionDto sessionDto);
        Task<bool> UpdateSessionAsync(int id, SessionDto sessionDto);
        Task RemoveSessionAsync(int id);
    }
}