using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Interfaces
{
	public interface IAuthService
	{
		Task SaveRefreshTokenAsync(string refreshToken, string userId);
        Task<bool> IsUserValid(LoginDTO userCredentials);
        Task<LoginResponseDTO> RegisterUser(RegisterDTO registerDTO, string role);
        Task<LoginResponseDTO> AuthenticateUserAsync(LoginDTO loginDTO);
        Task<LoginResponseDTO> RefreshAccessTokenAsync(string refreshToken);
        Task<LoginResponseDTO> RegisterAdminAsync(RegisterDTO registerDTO, string currentUserId);
        Task LogoutAsync(string refreshToken);
        Task<string> UpdateUserAsync(string username, UpdateUserDTO updateUserDTO);
        Task DeleteUserAsync(string username);
    }
}
