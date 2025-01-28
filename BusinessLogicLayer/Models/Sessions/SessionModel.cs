namespace BusinessLogic.Models.Sessions
{
    public class SessionModel
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public int HallId { get; set; }
        public string HallName { get; set; } = string.Empty; 
        public DateTime StartTime { get; set; }
        public double Price { get; set; }
    }
}