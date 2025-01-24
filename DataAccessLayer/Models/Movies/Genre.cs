using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Movies
{
    public class Genre
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Genre name is required")]
        [StringLength(50, ErrorMessage = "Genre name cannot exceed 50 characters")]
        public string Name { get; set; }

        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
