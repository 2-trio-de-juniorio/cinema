using BusinessLogic.Models.Sessions;

namespace BusinessLogicLayer.Interfaces
{
    public interface ISeatService
    {
        Task<int> CreateSeatAsync(SeatDTO seatDto);
        Task<List<SeatDTO>> GetAllSeatsAsync();
        Task<SeatDTO?> GetSeatByIdAsync(int id);
        Task<bool> UpdateSeatAsync(int id, SeatDTO seatDto);
        Task RemoveSeatAsync(int id);
    }
}
