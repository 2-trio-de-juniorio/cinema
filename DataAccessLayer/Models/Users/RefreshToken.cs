namespace DataAccess.Models.Users
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public required string Token { get; set; }
        public required string UserId { get; set; }
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; }
    }
}