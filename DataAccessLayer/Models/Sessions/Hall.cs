using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Sessions
{
    public class Hall
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Hall name is required")]
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
        public int Capacity { get; set; }

        public ICollection<Seat> Seats { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
