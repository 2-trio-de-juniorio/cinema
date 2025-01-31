namespace BusinessLogicLayer.DTOs
{
	public class LoginResponseDTO
	{
		public required string Id { get; init; } //todo: can be removed, because we can extract id from jwt token
		public required string AccessToken { get; init; }
		public required string RefreshToken { get; init; }
	}
}