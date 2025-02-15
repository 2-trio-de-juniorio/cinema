namespace BusinessLogic.Models.Tickets
{
    public class CreateTicketDTO
    {
        public string UserId { get; set; } = string.Empty;
        public int SessionId { get; set; }
        public List<int> SeatsId { get; set; } = new List<int>();
        public DateTime BookingDate { get; set; } = DateTime.UtcNow;
    }
}
