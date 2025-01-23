using DataAccess.Models.Movies.Actors;
using DataAccess.Models.Sessions;

namespace DataAccess.Models.Movies
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string TrailerUrl { get; set; }
        public string PosterUrl { get; set; }
        public double Rating { get; set; }

        public ICollection<MovieGenre> MovieGenres { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
