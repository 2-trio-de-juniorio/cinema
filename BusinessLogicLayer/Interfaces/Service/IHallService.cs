using BusinessLogicLayer.Dtos;

namespace BusinessLogicLayer.Interfaces
{
    public interface IHallService
    {
        Task<List<HallDto>> GetAllHallsAsync();
        Task<HallDto?> GetHallByIdAsync(int id);
        Task<int> CreateHallAsync(HallDto hallDto);
        Task<bool> UpdateHallAsync(int id, HallDto hallDto);
        Task<bool> DeleteHallAsync(int id);
    }
}
