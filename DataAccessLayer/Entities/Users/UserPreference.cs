using DataAccess.Models.Movies;

namespace DataAccess.Models.Users
{
    public class UserPreference
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public bool Liked { get; set; }
        public bool Watched { get; set; } 
        public double? Rating { get; set; } 
    }
}
