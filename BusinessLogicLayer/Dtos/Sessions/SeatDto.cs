namespace BusinessLogicLayer.Dtos
{
    public class SeatDto
    {
        public int Id { get; set; } 
        public int HallId { get; set; }
        public int RowNumber { get; set; } 
        public int SeatNumber { get; set; } 
        public bool IsBooked { get; set; }
        public string HallName { get; set; } 
    }
}
