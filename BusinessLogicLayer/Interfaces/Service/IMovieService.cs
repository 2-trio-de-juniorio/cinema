using BusinessLogic.Models.Movies;
using DataAccess.Models.Movies;

namespace BusinessLogicLayer.Interfaces
{
    public interface IMovieService 
    {
        Task<List<MovieDTO>> GetAllMoviesAsync();
        Task<MovieDTO?> GetMovieByIdAsync(int id);
        Task<int> CreateMovieAsync(CreateMovieDTO movieDto);
        Task<bool> UpdateMovieAsync(int id, CreateMovieDTO movieDto);
        Task RemoveMovieAsync(int id);
        Task<List<MovieDTO>> GetFilteredMoviesAsync(MovieFilterDTO filter);

    }
}