namespace DataAccessLayer.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public ICollection<MovieActor> MovieActors { get; set; }
    }
}
