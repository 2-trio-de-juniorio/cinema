namespace CinemaWebAPI.Services
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetSessionValue(string key, string value)
        {
            _httpContextAccessor.HttpContext?.Session.SetString(key, value);
        }

        public string? GetSessionValue(string key)
        {
            return _httpContextAccessor.HttpContext?.Session.GetString(key);
        }

        public void ClearSession()
        {
            _httpContextAccessor.HttpContext?.Session.Clear();
        }
    }

}
