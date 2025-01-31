namespace BusinessLogic.Models.Sessions
{
    public class CreateSessionDTO
    {
        public int MovieId { get; init; }
        public int HallId { get; init; }
        
        public DateTime StartTime { get; init; }
        public double Price { get; init; }
    }
}