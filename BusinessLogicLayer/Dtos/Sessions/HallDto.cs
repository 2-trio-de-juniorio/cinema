using DataAccess.Models.Sessions;

namespace BusinessLogicLayer.Dtos;

public class HallDto // replace with normal dto instead
{
    public List<SeatDto> Seats { get; set; }
    public string Name { get; set; }

    public static explicit operator HallDto(Hall hall)
    {
        return new HallDto()
        {
            Name = hall.Name,
            Seats = hall.Seats.Select(seat => new SeatDto() { IsBooked = seat.IsBooked, RowNumber = seat.RowNumber, SeatNumber = seat.SeatNumber })
            .ToList()
        };
    }
}