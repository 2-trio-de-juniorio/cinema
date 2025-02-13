using BusinessLogic.Models.Sessions;

namespace BusinessLogicLayer.Interfaces
{
    public interface ISessionService 
    {
        Task<List<SessionDTO>> GetAllSessionsAsync();
        Task<SessionDTO?> GetSessionByIdAsync(int id);
        Task<int> CreateSessionAsync(CreateSessionDTO createSessionDTO);
        Task<bool> UpdateSessionAsync(int id, CreateSessionDTO createSessionDTO);
        Task<bool> RemoveSessionAsync(int id);
        Task<List<SessionDTO>> GetFilteredSessionsAsync(SessionFilterDTO filter);
        Task<SessionsByMovieDTO> GetMoviesWithSessionsAsync(DateTime? date, string? genre, int page = 1, int pageSize = 6);
        Task<MovieSessionsDTO?> GetSessionsByMovieAsync(int movieId, DateTime? date);

    }
}