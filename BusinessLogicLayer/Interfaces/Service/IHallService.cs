using BusinessLogic.Models.Sessions;

namespace BusinessLogicLayer.Interfaces
{
    public interface IHallService
    {
        Task<List<HallDTO>> GetAllHallsAsync();
        Task<HallDTO?> GetHallByIdAsync(int id);
        Task<int> CreateHallAsync(CreateHallDTO createHallDto);
        Task<bool> UpdateHallAsync(int id, CreateHallDTO createHallDTO);
        Task<bool> DeleteHallAsync(int id);
    }
}
