namespace BusinessLogic.Models.Movies
{
    public class MovieDTO
    {
        public int Id { get; set; } //for admin update or delete
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Duration { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string TrailerUrl { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public double Rating { get; set; }

        public List<ActorDTO> Actors { get; set; } = [];
        public List<GenreDTO> Genres { get; set; } = [];
    }
}
