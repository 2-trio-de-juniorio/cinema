using BusinessLogic.Models.Movies;

namespace BusinessLogic.Models.Sessions
{
    public class SessionDTO
    {
        public int Id { get; set; }
        public MovieDTO Movie { get; set; }
        public HallDTO Hall { get; set; }
        
        public DateTime StartTime { get; set; }
        public double Price { get; set; }
    }
}