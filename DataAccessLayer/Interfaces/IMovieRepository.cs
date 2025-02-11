// DataAccessLayer/Interfaces/IMovieRepository.cs
using DataAccess.Models.Movies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<List<Movie>> GetAllMoviesAsync();
    }
}
