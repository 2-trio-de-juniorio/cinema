using DataAccess.Models.Tickets;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Sessions
{
    public class Seat
    {
        public int Id { get; set; }
        public int HallId { get; set; }
        public Hall Hall { get; set; }

        [Required(ErrorMessage = "RowNumber is required")]
        [Range(1, int.MaxValue, ErrorMessage = "RowNumber must be greater than 0")]
        public int RowNumber { get; set; }

        [Required(ErrorMessage = "SeatNumber is required")]
        [Range(1, int.MaxValue, ErrorMessage = "SeatNumber must be greater than 0")]
        public int SeatNumber { get; set; }
        public bool IsBooked { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
