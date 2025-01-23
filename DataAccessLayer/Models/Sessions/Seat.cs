using DataAccess.Models.Tickets;

namespace DataAccess.Models.Sessions
{
    public class Seat
    {
        public int Id { get; set; }
        public int HallId { get; set; }
        public Hall Hall { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public bool IsBooked { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
