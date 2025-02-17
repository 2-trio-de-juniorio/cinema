namespace BusinessLogic.Models.Sessions
{
    public class CreateHallDTO
    {
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }

        public List<CreateSeatDTO> Seats { get; set; } = new List<CreateSeatDTO>();
    }
}
