// DataAccessLayer/Repositories/MovieRepository.cs
using DataAccess.Models.Movies;
using DataAccessLayer.Data;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            return await _entities.ToListAsync();
        }
    }
}
