using DataAccess.Models.Sessions;

namespace BusinessLogicLayer.Dtos;

public class SessionDto // replace with normal dto instead
{
    public MovieDto Movie { get; set; }
    public HallDto Hall { get; set; }
    public DateTime StartTime { get; set; }
    public double Price { get; set; }

    public static explicit operator SessionDto(Session session)
    {
        return new SessionDto()
        {
            Movie = (MovieDto)session.Movie,
            Hall = new HallDto()
            {
                Name = session.Hall.Name,
                Seats = session.Hall.Seats.Select(seat => new SeatDto() { IsBooked = seat.IsBooked, RowNumber = seat.RowNumber, SeatNumber = seat.SeatNumber })
                    .ToList()
            }
        };
    }
}