using BusinessLogic.Models.Sessions;

namespace BusinessLogicLayer.Interfaces
{
    public interface IHallService
    {
        Task<List<HallDTO>> GetAllHallsAsync();
        Task<HallDTO?> GetHallByIdAsync(int id);
        Task<int> CreateHallAsync(HallDTO hallDto);
        Task<bool> UpdateHallAsync(int id, HallDTO hallDto);
        Task<bool> DeleteHallAsync(int id);
    }
}
