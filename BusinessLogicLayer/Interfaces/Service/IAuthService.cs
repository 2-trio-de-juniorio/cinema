using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Interfaces
{
	public interface IAuthService
	{
		Task SaveRefreshTokenAsync(string refreshToken, string userId);
        Task<bool> IsUserValid(LoginDTO userCredentials);
        Task<string?> RegisterUser(RegisterDTO registerDTO);
        Task<LoginResponseDTO> AuthenticateUserAsync(LoginDTO loginDTO);
        Task<LoginResponseDTO> RefreshAccessTokenAsync(string refreshToken);
	}
}
