namespace CinemaWebAPI.Services
{
    public interface ISessionService
    {
        void SetSessionValue(string key, string value);
        string? GetSessionValue(string key);
        void ClearSession();
    }
}
