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

        public async Task<int> CreateSeatAsync(SeatDTO seatDto)
        {
            var seat = _mapper.Map<Seat>(seatDto);
            await _unitOfWork.GetRepository<Seat>().AddAsync(seat);
            await _unitOfWork.SaveAsync();
            return seat.Id;
        }

        public async Task<List<SeatDTO>> GetAllSeatsAsync()
        {
            var seats = await _unitOfWork.GetRepository<Seat>().GetAllAsync();
            return seats.Select(seat => _mapper.Map<SeatDTO>(seat)).ToList();
        }

        public async Task<SeatDTO?> GetSeatByIdAsync(int id)
        {
            var seat = await _unitOfWork.GetRepository<Seat>().GetByIdAsync(id);
            return seat != null ? _mapper.Map<SeatDTO>(seat) : null;
        }

        public async Task<bool> UpdateSeatAsync(int id, SeatDTO seatDto)
        {
            var seat = await _unitOfWork.GetRepository<Seat>().GetByIdAsync(id);
            if (seat == null)
                return false;

            _mapper.Map(seatDto, seat);
            _unitOfWork.GetRepository<Seat>().Update(seat);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task RemoveSeatAsync(int id)
        {
            await _unitOfWork.GetRepository<Seat>().RemoveByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
