using DataAccessLayer.Interfaces;
using BusinessLogicLayer.Interfaces;
using DataAccess.Models.Sessions;
using BusinessLogic.Models.Sessions;

namespace BusinessLogicLayer.Services
{
    internal sealed class SeatService : ISeatService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SeatService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateSeatAsync(SeatDTO seatDto)
        {
            // Находим зал по названию
            var hallRepository = _unitOfWork.GetRepository<Hall>();
            var hallList = await hallRepository.GetAllAsync(); // Загружаем все залы
            var hall = hallList.FirstOrDefault(h => h.Name == seatDto.HallName);

            if (hall == null)
            {
                throw new ArgumentException($"Hall with name '{seatDto.HallName}' does not exist.");
            }

            var seat = new Seat()
            {
                RowNumber = seatDto.RowNumber,
                SeatNumber = seatDto.SeatNumber,
                IsBooked = seatDto.IsBooked,
                HallId = hall.Id // Используем найденный ID зала
            };

            await _unitOfWork.GetRepository<Seat>().AddAsync(seat);
            await _unitOfWork.SaveAsync();

            return seat.Id;
        }

        public async Task<List<SeatDTO>> GetAllSeatsAsync()
        {
            var seats = await _unitOfWork.GetRepository<Seat>().GetAllAsync();
            return seats.Select(seat => new SeatDTO
            {
                Id = seat.Id,
                RowNumber = seat.RowNumber,
                SeatNumber = seat.SeatNumber,
                IsBooked = seat.IsBooked,
                HallName = seat.Hall?.Name  // Подгружаем имя зала
            }).ToList();
        }

        public async Task<SeatDTO?> GetSeatByIdAsync(int id)
        {
            var seat = await _unitOfWork.GetRepository<Seat>().GetByIdAsync(id);
            if (seat == null)
                return null;

            return new SeatDTO
            {
                Id = seat.Id,
                RowNumber = seat.RowNumber,
                SeatNumber = seat.SeatNumber,
                IsBooked = seat.IsBooked,
                HallName = seat.Hall?.Name
            };
        }

        public async Task<bool> UpdateSeatAsync(int id, SeatDTO seatDto)
        {
            var seat = await _unitOfWork.GetRepository<Seat>().GetByIdAsync(id);
            if (seat == null)
                return false;

            seat.RowNumber = seatDto.RowNumber;
            seat.SeatNumber = seatDto.SeatNumber;
            seat.IsBooked = seatDto.IsBooked;

            // Если передано новое название зала, обновляем привязку
            if (!string.IsNullOrEmpty(seatDto.HallName))
            {
                var hallRepository = _unitOfWork.GetRepository<Hall>();
                var hallList = await hallRepository.GetAllAsync();
                var hall = hallList.FirstOrDefault(h => h.Name == seatDto.HallName);

                if (hall == null)
                {
                    throw new ArgumentException($"Hall with name '{seatDto.HallName}' does not exist.");
                }

                seat.HallId = hall.Id;
            }

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
