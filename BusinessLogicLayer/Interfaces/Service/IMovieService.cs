using BusinessLogic.Models.Movies;

namespace BusinessLogicLayer.Interfaces
{
    public interface IMovieService 
    {
        Task<List<MovieDTO>> GetAllMoviesAsync();
        Task<MovieDTO?> GetMovieByIdAsync(int id);
        Task<int> CreateMovieAsync(CreateMovieDTO createMovieDTO);
        Task<bool> UpdateMovieAsync(int id, CreateMovieDTO createMovieDTO);
        Task RemoveMovieAsync(int id);
        Task<List<MovieDTO>> GetFilteredMoviesAsync(MovieFilterDTO filter);
        Task<List<MovieDTO>> GetSimilarMoviesAsync(int movieId);

    }
}