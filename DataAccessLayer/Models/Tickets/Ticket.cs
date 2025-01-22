namespace DataAccessLayer.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public int SessionId { get; set; }
        public Session Session { get; set; }
        public int SeatId { get; set; }
        public Seat Seat { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
