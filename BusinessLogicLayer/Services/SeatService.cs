using AutoMapper;
using DataAccessLayer.Interfaces;
using BusinessLogicLayer.Interfaces;
using DataAccess.Models.Sessions;
using BusinessLogic.Models.Sessions;

namespace BusinessLogicLayer.Services
{
    internal sealed class SeatService : ISeatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SeatService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateSeatAsync(CreateSeatDTO createSeatDTO)
        {
            await checkSeatDTO(createSeatDTO);

            var seat = _mapper.Map<Seat>(createSeatDTO);

            await _unitOfWork.GetRepository<Seat>().AddAsync(seat);
            await _unitOfWork.SaveAsync();

            return seat.Id;
        }

        public async Task<List<SeatDTO>> GetAllSeatsAsync()
        {
            return _mapper.Map<List<Seat>, List<SeatDTO>>(
                await _unitOfWork.GetRepository<Seat>().GetAllAsync());
        }

        public async Task<SeatDTO?> GetSeatByIdAsync(int id)
        {
            Seat? seat = await _unitOfWork.GetRepository<Seat>().GetByIdAsync(id);
            return seat != null ? _mapper.Map<SeatDTO>(seat) : null;
        }

        public async Task<bool> UpdateSeatAsync(int id, CreateSeatDTO createSeatDTO)
        {
            await checkSeatDTO(createSeatDTO);

            Seat? seat = await _unitOfWork.GetRepository<Seat>().GetByIdAsync(id);

            if (seat == null) return false;

            _mapper.Map(createSeatDTO, seat);

            _unitOfWork.GetRepository<Seat>().Update(seat);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task RemoveSeatAsync(int id)
        {
            await _unitOfWork.GetRepository<Seat>().RemoveByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        private async Task checkSeatDTO(CreateSeatDTO createSeatDTO) 
        {
            Hall? hall = await _unitOfWork.GetRepository<Hall>().GetByIdAsync(createSeatDTO.HallId);
            if (hall == null) 
                throw new ArgumentException($"Hall with id {createSeatDTO.HallId} does not exist");
        }
    }
}
