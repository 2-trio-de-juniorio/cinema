using AutoMapper;
using BusinessLogic.Models.Sessions;
using BusinessLogicLayer.Interfaces;
using DataAccess.Models.Sessions;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services;

internal sealed class HallService : IHallService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    private readonly string[] HallEntityIncludes =
    [
        nameof(Hall.Seats)
    ];

    public HallService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<HallDTO>> GetAllHallsAsync()
    {
        var halls = await _unitOfWork.GetRepository<Hall>().GetAllAsync(HallEntityIncludes);
        return halls.Select(h => _mapper.Map<HallDTO>(h)).ToList();
    }

    public async Task<HallDTO?> GetHallByIdAsync(int id)
    {
        var hall = await _unitOfWork.GetRepository<Hall>().GetByIdAsync(id, HallEntityIncludes);
        return hall != null ? _mapper.Map<HallDTO>(hall) : null;
    }

    public async Task<int> CreateHallAsync(HallDTO hallDTO)
    {
        var hall = new Hall
        {
            Name = hallDTO.Name,
            Seats = hallDTO.Seats.Select(seatDto => new Seat
            {
                RowNumber = seatDto.RowNumber,
                SeatNumber = seatDto.SeatNumber,
                IsBooked = seatDto.IsBooked
            }).ToList()
        };

        await _unitOfWork.GetRepository<Hall>().AddAsync(hall);
        await _unitOfWork.SaveAsync();
        return hall.Id;
    }

    public async Task<bool> UpdateHallAsync(int id, HallDTO hallDTO)
    {
        var hall = await _unitOfWork.GetRepository<Hall>().GetByIdAsync(id, HallEntityIncludes);
        if (hall == null) return false;

        hall.Name = hallDTO.Name;
        hall.Seats = hallDTO.Seats.Select(seatDto => new Seat
        {
            RowNumber = seatDto.RowNumber,
            SeatNumber = seatDto.SeatNumber,
            IsBooked = seatDto.IsBooked
        }).ToList();

        _unitOfWork.GetRepository<Hall>().Update(hall);
        await _unitOfWork.SaveAsync();
        return true;
    }

    public async Task<bool> DeleteHallAsync(int id)
    {
        bool success = await _unitOfWork.GetRepository<Hall>().RemoveByIdAsync(id);
        await _unitOfWork.SaveAsync();
        return success;
    }
}
