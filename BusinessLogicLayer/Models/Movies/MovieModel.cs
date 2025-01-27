namespace BusinessLogic.Models.Movies
{
    public class MovieModel
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Duration { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string TrailerUrl { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public double Rating { get; set; }

        public List<ActorModel> Actors { get; set; } = new List<ActorModel>();
        public List<GenreModel> Genres { get; set; } = new List<GenreModel>();
    }
}
