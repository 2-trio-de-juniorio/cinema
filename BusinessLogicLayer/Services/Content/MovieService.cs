using AutoMapper;
using BusinessLogic.Models.Movies;
using BusinessLogicLayer.Interfaces;
using DataAccess.Models.Movies;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BusinessLogicLayer.Services
{
    internal sealed class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly string[] MovieEntityIncludes = ["MovieActors.Actor", "MovieGenres.Genre"];
        
        public MovieService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateMovieAsync(CreateMovieDTO MovieDTO)
        {
            Movie movie = _mapper.Map<Movie>(MovieDTO);

            await _unitOfWork.GetRepository<Movie>().AddAsync(movie);
            await _unitOfWork.SaveAsync();

            return movie.Id;
        }

        public async Task<List<MovieDTO>> GetAllMoviesAsync()
        {
            return (await _unitOfWork.GetRepository<Movie>().GetAllAsync(MovieEntityIncludes))
                .Select(m => _mapper.Map<MovieDTO>(m))
                .ToList();
        }

        public async Task<MovieDTO?> GetMovieByIdAsync(int id)
        {
            Movie? movie = await GetMovieEntityByIdAsync(id);
            return movie != null ? _mapper.Map<MovieDTO>(movie) : null;
        }

        public async Task RemoveMovieAsync(int id)
        {
            await _unitOfWork.GetRepository<Movie>().RemoveByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> UpdateMovieAsync(int id, CreateMovieDTO MovieDTO)
        {
            Movie? movie = await GetMovieEntityByIdAsync(id);

            if (movie == null) return false;

            movie.MovieGenres.Clear();
            movie.MovieActors.Clear();

            _mapper.Map(MovieDTO, movie);

            _unitOfWork.GetRepository<Movie>().Update(movie);
            await _unitOfWork.SaveAsync();

            return true;
        }

        private async Task<Movie?> GetMovieEntityByIdAsync(int id)
        {
            return await _unitOfWork.GetRepository<Movie>().GetByIdAsync(id, MovieEntityIncludes);
        }

        public async Task<List<MovieDTO>> GetFilteredMoviesAsync(MovieFilterDTO filter)
        {
            var movies = await _unitOfWork.GetRepository<Movie>().GetAllAsync(MovieEntityIncludes);

            // Filtering by genre
            if (!string.IsNullOrEmpty(filter.Genre))
            {
                movies = movies.Where(m => m.MovieGenres
                .Any(mg => mg.Genre.Name.Equals(filter.Genre, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }

            // Sorting
            movies = filter.SortBy?.ToLower() switch
            {
                "date_asc" => movies.OrderBy(m => m.ReleaseDate).ToList(),
                "date_desc" => movies.OrderByDescending(m => m.ReleaseDate).ToList(),
                "rating_asc" => movies.OrderBy(m => m.Rating).ToList(),
                "rating_desc" => movies.OrderByDescending(m => m.Rating).ToList(),
                _ => movies.OrderByDescending(m => m.ReleaseDate).ToList()
            };

            return movies.Select(m => _mapper.Map<MovieDTO>(m)).ToList();
        }

    }
}