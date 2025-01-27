using BusinessLogic.Models.Movies;

namespace BusinessLogic.Models.Sessions
{
    public class SessionModel
    {
        public MovieModel? Movie { get; set; }
        public HallModel? Hall { get; set; }
        public DateTime StartTime { get; set; }
        public double Price { get; set; }
    }
}