namespace BusinessLogic.Models.Movies
{
    public class MovieFilterDTO
    {
        public string? Genre { get; set; }

        public string? SortBy { get; set; } // "date", "rating"
        public string? SortOrder { get; set; } // "asc", "desc"
        public int? Page { get; set; }
    }
}
