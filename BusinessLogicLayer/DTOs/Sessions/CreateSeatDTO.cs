namespace BusinessLogic.Models.Sessions
{
    public class CreateSeatDTO
    {
        public int HallId { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
    }
}