using BusinessLogicLayer.Dtos;

namespace BusinessLogicLayer.Interfaces
{
    public interface ISeatService
    {
        Task<int> CreateSeatAsync(SeatDto seatDto);
        Task<List<SeatDto>> GetAllSeatsAsync();
        Task<SeatDto?> GetSeatByIdAsync(int id);
        Task<bool> UpdateSeatAsync(int id, SeatDto seatDto);
        Task RemoveSeatAsync(int id);
    }
}
