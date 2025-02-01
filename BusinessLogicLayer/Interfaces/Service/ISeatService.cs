using BusinessLogic.Models.Sessions;

namespace BusinessLogicLayer.Interfaces
{
    public interface ISeatService
    {
        Task<int> CreateSeatAsync(CreateSeatDTO createSeatDTO);
        Task<List<SeatDTO>> GetAllSeatsAsync();
        Task<SeatDTO?> GetSeatByIdAsync(int id);
        Task<bool> UpdateSeatAsync(int id, CreateSeatDTO createSeatDTO);
        Task RemoveSeatAsync(int id);
    }
}
