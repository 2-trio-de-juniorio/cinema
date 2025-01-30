using BusinessLogic.Models.Movies;

namespace BusinessLogicLayer.Interfaces
{
    public interface IMovieService 
    {
        Task<List<MovieDTO>> GetAllMoviesAsync();
        Task<MovieDTO?> GetMovieByIdAsync(int id);
        Task<int> CreateMovieAsync(MovieDTO movieDto);
        Task<bool> UpdateMovieAsync(int id, MovieDTO movieDto);
        Task RemoveMovieAsync(int id);
    }
}