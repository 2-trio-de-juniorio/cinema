namespace BusinessLogic.Models.Sessions
{
    public class SessionPreviewDTO
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime DateTime { get; set; }
        public int? AvailableSeatsCount { get; set; }
    }
}
