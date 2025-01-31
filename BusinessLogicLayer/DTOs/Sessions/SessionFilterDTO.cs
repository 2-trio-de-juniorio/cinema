namespace BusinessLogic.Models.Sessions
{
    public class SessionFilterDTO
    {
        public DateTime? Date { get; set; }  // filter by a specific date
        public string? SortBy { get; set; } // "price_asc", "price_desc"

    }
}