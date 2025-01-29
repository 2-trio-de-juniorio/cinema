namespace BusinessLogicLayer.Interfaces
{
    public interface ITokenGeneratorService
    {
        string GenerateAccessToken(string userId, string role);
        string GenerateRefreshToken();
    }
}
