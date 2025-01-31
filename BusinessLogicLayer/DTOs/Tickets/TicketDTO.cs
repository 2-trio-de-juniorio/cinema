namespace BusinessLogic.Models.Tickets
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int SessionId { get; set; }
        public DateTime StartTime { get; set; } 
        public string MovieTitle { get; set; } = string.Empty; 
        public int SeatId { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public DateTime BookingDate { get; set; } 
    }
}
