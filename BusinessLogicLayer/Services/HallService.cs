using BusinessLogicLayer.Dtos;
using BusinessLogicLayer.Interfaces;
using DataAccess.Models.Sessions;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Services;

public class HallService : IHallService
{
    private readonly AppDbContext _context;

    public HallService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<HallDto>> GetAllHallsAsync()
    {
        var halls = await _context.Halls
            .Include(h => h.Seats)
            .ToListAsync();

        return halls.Select(hall => (HallDto)hall).ToList();
    }

    public async Task<HallDto?> GetHallByIdAsync(int id)
    {
        var hall = await _context.Halls
            .Include(h => h.Seats)
            .FirstOrDefaultAsync(h => h.Id == id);

        return hall != null ? (HallDto)hall : null;
    }

    public async Task<int> CreateHallAsync(HallDto hallDto)
    {
        var hall = new Hall
        {
            Name = hallDto.Name,
            Seats = hallDto.Seats.Select(seatDto => new Seat
            {
                RowNumber = seatDto.RowNumber,
                SeatNumber = seatDto.SeatNumber,
                IsBooked = seatDto.IsBooked
            }).ToList()
        };

        _context.Halls.Add(hall);
        await _context.SaveChangesAsync();
        return hall.Id;
    }

    public async Task<bool> UpdateHallAsync(int id, HallDto hallDto)
    {
        var hall = await _context.Halls
            .Include(h => h.Seats)
            .FirstOrDefaultAsync(h => h.Id == id);

        if (hall == null)
        {
            return false;
        }

        hall.Name = hallDto.Name;
        hall.Seats = hallDto.Seats.Select(seatDto => new Seat
        {
            RowNumber = seatDto.RowNumber,
            SeatNumber = seatDto.SeatNumber,
            IsBooked = seatDto.IsBooked
        }).ToList();

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteHallAsync(int id)
    {
        var hall = await _context.Halls.FindAsync(id);

        if (hall == null)
        {
            return false;
        }

        _context.Halls.Remove(hall);
        await _context.SaveChangesAsync();
        return true;
    }
}
