using DataAccess.Models.Movies.Actors;
using DataAccess.Models.Sessions;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Movies
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Range(1, 300, ErrorMessage = "Duration must be between 1 and 300 minutes")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Release date is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateTime ReleaseDate { get; set; }

        [Url(ErrorMessage = "TrailerUrl must be a valid URL")]
        public string TrailerUrl { get; set; }

        [Url(ErrorMessage = "PosterUrl must be a valid URL")]
        public string PosterUrl { get; set; }

        [Range(0, 10, ErrorMessage = "Rating must be between 0 and 10")]
        public double Rating { get; set; }

        public ICollection<MovieGenre> MovieGenres { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
