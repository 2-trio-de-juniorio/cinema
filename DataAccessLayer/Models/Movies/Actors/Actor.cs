using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Movies.Actors
{
    public class Actor
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public ICollection<MovieActor> MovieActors { get; set; }
    }
}
