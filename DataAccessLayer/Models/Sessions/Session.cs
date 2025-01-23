using DataAccess.Models.Movies;
using DataAccess.Models.Tickets;

namespace DataAccess.Models.Sessions
{
    public class Session
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int HallId { get; set; }
        public Hall Hall { get; set; }
        public DateTime StartTime { get; set; }
        public double Price { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
