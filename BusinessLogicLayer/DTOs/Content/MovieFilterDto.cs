namespace BusinessLogic.Models.Movies
{
    public class MovieFilterDTO
    {
        public string? Genre { get; set; }

        public string? SortBy { get; set; } // "date_asc", "date_desc", "rating_asc", "rating_desc"
        public int? Page { get; set; }
    }
}
