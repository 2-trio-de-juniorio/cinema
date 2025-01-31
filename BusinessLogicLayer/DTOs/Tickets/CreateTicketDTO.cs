namespace BusinessLogic.Models.Tickets
{
    public class CreateTicketDTO
    {
        public string UserId { get; set; } = string.Empty;
        public int SessionId { get; set; }
        public int SeatId { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.UtcNow;
    }
}
