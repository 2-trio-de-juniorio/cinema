using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Interfaces
{
	public interface IAuthService
	{
		Task SaveRefreshTokenAsync(string refreshToken, string userId);
		Task<bool> IsUserValid(string username, string password);
		Task<(string AccessToken, string RefreshToken)> RefreshAccessTokenAsync(string refreshToken);
	}
}
