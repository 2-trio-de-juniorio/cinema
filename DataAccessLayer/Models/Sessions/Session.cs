using DataAccess.Models.Movies;
using DataAccess.Models.Tickets;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Sessions
{
    public class Session
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int HallId { get; set; }
        public Hall Hall { get; set; }
       
        [Required(ErrorMessage = "Start time is required")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }
        
        [Required(ErrorMessage = "Price is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double Price { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
