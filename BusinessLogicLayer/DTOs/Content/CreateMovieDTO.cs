namespace BusinessLogic.Models.Movies
{
    public class CreateMovieDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Duration { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string TrailerUrl { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public double Rating { get; set; }

        public List<int> ActorsIds { get; set; } = [];
        public List<int> GenresIds { get; set; } = [];
    }
}