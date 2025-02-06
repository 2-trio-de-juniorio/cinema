using BusinessLogic.Models.Movies;

namespace BusinessLogicLayer.Interfaces
{
    public interface IGenreService
    {
        Task<List<GenreDTO>> GetAllGenresAsync();
        Task<GenreDTO?> GetGenreByIdAsync(int id);
        Task<int> CreateGenreAsync(CreateGenreDTO createGenreDTO);
        Task<bool> UpdateGenreAsync(int id, CreateGenreDTO createGenreDTO);
        Task<bool> RemoveGenreAsync(int id);
    }
}