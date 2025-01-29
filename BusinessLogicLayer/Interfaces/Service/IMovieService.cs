using BusinessLogicLayer.Dtos;

namespace BusinessLogicLayer.Interfaces;

public interface IMovieService 
{
    Task<List<MovieDto>> GetAllMoviesAsync();
    Task<MovieDto?> GetMovieByIdAsync(int id);
    Task<int> CreateMovieAsync(MovieDto movieDto);
    Task<bool> UpdateMovieAsync(int id, MovieDto movieDto);
    Task RemoveMovieAsync(int id);
}