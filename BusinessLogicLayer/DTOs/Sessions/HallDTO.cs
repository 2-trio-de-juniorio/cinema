namespace BusinessLogic.Models.Sessions
{
    public class HallDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }

        public List<SeatDTO> Seats { get; set; } = new List<SeatDTO>();
        public HallSeatsDTO HallSeats { get; set; } = new HallSeatsDTO();

    }
}

