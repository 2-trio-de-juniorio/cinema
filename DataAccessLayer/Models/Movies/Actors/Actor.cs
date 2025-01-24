using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Movies.Actors
{
    public class Actor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        [StringLength(100, ErrorMessage = "Firstname cannot exceed 100 characters")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        [StringLength(100, ErrorMessage = "Lastname cannot exceed 100 characters")]
        public string Lastname { get; set; }

        public ICollection<MovieActor> MovieActors { get; set; }
    }
}
