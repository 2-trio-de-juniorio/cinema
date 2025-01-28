namespace BusinessLogicLayer.DTOs
{
	public class LoginResponseDTO
	{
		public required string AccessToken { get; init; }
		public required string RefreshToken { get; init; }
	}
}