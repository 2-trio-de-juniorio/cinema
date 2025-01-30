namespace BusinessLogic.Models.Sessions
{
    public class HallModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }

        public List<SeatModel> Seats { get; set; } = new List<SeatModel>();
    }
}

