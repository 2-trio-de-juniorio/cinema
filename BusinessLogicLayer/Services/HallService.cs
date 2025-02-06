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
        return _mapper.Map<List<Hall>, List<HallDTO>>(
            await _unitOfWork.GetRepository<Hall>().GetAllAsync(HallEntityIncludes));
    }

    public async Task<HallDTO?> GetHallByIdAsync(int id)
    {
        Hall? hall = await GetHallEntityByIdAsync(id);
        return hall != null ? _mapper.Map<HallDTO>(hall) : null;
    }

    public async Task<int> CreateHallAsync(CreateHallDTO createHallDTO)
    {
        Hall hall = _mapper.Map<Hall>(createHallDTO);

        await _unitOfWork.GetRepository<Hall>().AddAsync(hall);
        await _unitOfWork.SaveAsync();

        return hall.Id;
    }

    public async Task<bool> UpdateHallAsync(int id, CreateHallDTO createHallDTO)
    {
        Hall? hall = await GetHallEntityByIdAsync(id);

        if (hall == null) return false;

        _mapper.Map(createHallDTO, hall);

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

    private Task<Hall?> GetHallEntityByIdAsync(int id) 
    {
        return _unitOfWork.GetRepository<Hall>().GetByIdAsync(id, HallEntityIncludes);
    }
}
