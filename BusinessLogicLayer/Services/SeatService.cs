using DataAccessLayer.Interfaces;
using BusinessLogicLayer.Dtos;
using BusinessLogicLayer.Interfaces;
using DataAccess.Models.Sessions;

namespace BusinessLogicLayer.Services
{
    internal sealed class SeatService : ISeatService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SeatService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateSeatAsync(SeatDto seatDto)
        {
            var seat = new Seat()
            {
                RowNumber = seatDto.RowNumber,
                SeatNumber = seatDto.SeatNumber,
                IsBooked = seatDto.IsBooked,
                HallId = seatDto.HallId
            };

            await _unitOfWork.GetRepository<Seat>().AddAsync(seat);
            await _unitOfWork.SaveAsync();

            return seat.Id;
        }

        public async Task<List<SeatDto>> GetAllSeatsAsync()
        {
            var seats = await _unitOfWork.GetRepository<Seat>().GetAllAsync();
            return seats.Select(seat => new SeatDto
            {
                Id = seat.Id,
                RowNumber = seat.RowNumber,
                SeatNumber = seat.SeatNumber,
                IsBooked = seat.IsBooked,
                HallId = seat.HallId,
                HallName = seat.Hall?.Name 
            }).ToList();
        }

        public async Task<SeatDto?> GetSeatByIdAsync(int id)
        {
            var seat = await _unitOfWork.GetRepository<Seat>().GetByIdAsync(id);
            if (seat == null)
                return null;

            return new SeatDto
            {
                Id = seat.Id,
                RowNumber = seat.RowNumber,
                SeatNumber = seat.SeatNumber,
                IsBooked = seat.IsBooked,
                HallId = seat.HallId,
                HallName = seat.Hall?.Name
            };
        }

        public async Task<bool> UpdateSeatAsync(int id, SeatDto seatDto)
        {
            var seat = await _unitOfWork.GetRepository<Seat>().GetByIdAsync(id);
            if (seat == null)
                return false;

            seat.RowNumber = seatDto.RowNumber;
            seat.SeatNumber = seatDto.SeatNumber;
            seat.IsBooked = seatDto.IsBooked;
            seat.HallId = seatDto.HallId;

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
