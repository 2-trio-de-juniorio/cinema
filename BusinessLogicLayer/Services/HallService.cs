using AutoMapper;
using BusinessLogic.Models.Sessions;
using BusinessLogicLayer.Interfaces;
using DataAccess.Models.Sessions;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Services;

public class HallService : IHallService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public HallService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<HallDTO>> GetAllHallsAsync()
    {
        var halls = await _context.Halls
            .Include(h => h.Seats)
            .ToListAsync();

        return halls.Select(hall => _mapper.Map<HallDTO>(hall)).ToList();
    }

    public async Task<HallDTO?> GetHallByIdAsync(int id)
    {
        var hall = await _context.Halls
            .Include(h => h.Seats)
            .FirstOrDefaultAsync(h => h.Id == id);

        return hall != null ? _mapper.Map<HallDTO>(hall) : null;
    }

    public async Task<int> CreateHallAsync(HallDTO HallDTO)
    {
        var hall = new Hall
        {
            Name = HallDTO.Name,
            Seats = HallDTO.Seats.Select(seatDto => new Seat
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

    public async Task<bool> UpdateHallAsync(int id, HallDTO HallDTO)
    {
        var hall = await _context.Halls
            .Include(h => h.Seats)
            .FirstOrDefaultAsync(h => h.Id == id);

        if (hall == null)
        {
            return false;
        }

        hall.Name = HallDTO.Name;
        hall.Seats = HallDTO.Seats.Select(seatDto => new Seat
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
