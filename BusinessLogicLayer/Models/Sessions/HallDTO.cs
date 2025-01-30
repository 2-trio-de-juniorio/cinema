namespace BusinessLogic.Models.Sessions
{
    public class HallDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }

        public List<SeatModel> Seats { get; set; } = new List<SeatModel>();
    }
}

