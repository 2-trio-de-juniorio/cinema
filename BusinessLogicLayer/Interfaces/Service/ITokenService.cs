using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(string userId, string role);
        string GenerateRefreshToken();
    }
}
