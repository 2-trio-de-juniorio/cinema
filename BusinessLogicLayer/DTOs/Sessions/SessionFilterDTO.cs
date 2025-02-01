namespace BusinessLogic.Models.Sessions
{
    public class SessionFilterDTO
    {
        public DateTime? Date { get; set; }  // filter by a specific date
        public string? SortBy { get; set; } // "price", "time"
        public string? SortOrder { get; set; } // "asc", "desc"
        public int? Page { get; set; }
    }
}