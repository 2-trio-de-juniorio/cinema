namespace BusinessLogic.Models.Users
{
    public class UserPreferenceDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int MovieId { get; set; }
        public bool Liked { get; set; }
        public bool Watched { get; set; }
        public double? Rating { get; set; }
    }
}
