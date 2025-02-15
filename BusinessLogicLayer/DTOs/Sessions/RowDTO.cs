namespace BusinessLogic.Models.Sessions
{
    public class RowDTO
    {
        public int Count { get; set; }
        public Dictionary<int, bool> Seats { get; set; } = new Dictionary<int, bool>();
    }
}
