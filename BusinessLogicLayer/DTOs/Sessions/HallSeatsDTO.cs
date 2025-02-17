namespace BusinessLogic.Models.Sessions
{
    public class HallSeatsDTO
    {
        public int Rows { get; set; }
        public List<RowDTO> Columns { get; set; } = new List<RowDTO>();
    }
}
