namespace BusinessLogic.Models.Sessions
{
    public class SessionDTO
    {
        public int Id { get; init; }
        public int MovieId { get; init; }
        public string MovieTitle { get; init; } = string.Empty;
        public int HallId { get; init; }
        public string HallName { get; init; } = string.Empty; 
        public DateTime StartTime { get; init; }
        public double Price { get; init; }
    }
}