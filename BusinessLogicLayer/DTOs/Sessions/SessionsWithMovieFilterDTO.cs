namespace BusinessLogic.Models.Sessions
{
    public class SessionWithMovieFilterDTO
    {
        public DateTime? Date { get; set; }
        public int? GenreId { get; set; }
        public int Page { get; set; } = 1;
    }
}