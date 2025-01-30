namespace BusinessLogic.Models.Movies
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Duration { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string TrailerUrl { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public double Rating { get; set; }

        public List<int> ActorIds { get; set; } = new List<int>();
        public List<int> GenreIds { get; set; } = new List<int>();
    }
}
