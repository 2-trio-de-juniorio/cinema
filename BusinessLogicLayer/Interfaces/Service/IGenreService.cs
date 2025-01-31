using BusinessLogic.Models.Movies;

namespace BusinessLogicLayer.Interfaces
{
    public interface IGenreService
    {
        Task<List<GenreDTO>> GetAllGenresAsync();
        Task<GenreDTO?> GetGenreByIdAsync(int id);
        Task<int> CreateGenreAsync(CreateGenreDTO actorDTO);
        Task<bool> UpdateGenreAsync(int id, CreateGenreDTO actorDTO);
        Task<bool> RemoveGenreAsync(int id);
    }
}