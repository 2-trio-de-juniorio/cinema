using AutoMapper;
using BusinessLogic.Models.Movies;
using BusinessLogicLayer.Interfaces;
using DataAccess.Models.Movies;
using DataAccess.Models.Movies.Actors;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    internal sealed class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly string[] MovieEntityIncludes = 
        [
            $"{nameof(Movie.MovieGenres)}.{nameof(MovieGenre.Genre)}", 
            $"{nameof(Movie.MovieActors)}.{nameof(MovieActor.Actor)}"
        ];

        public MovieService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateMovieAsync(CreateMovieDTO createMovieDTO)
        {
            await CheckMovieDTO(createMovieDTO);

            Movie movie = _mapper.Map<Movie>(createMovieDTO);

            await _unitOfWork.GetRepository<Movie>().AddAsync(movie);
            await _unitOfWork.SaveAsync();

            return movie.Id;
        }

        public async Task<List<MovieDTO>> GetAllMoviesAsync()
        {
            return _mapper.Map<List<Movie>, List<MovieDTO>>(
                await _unitOfWork.GetRepository<Movie>().GetAllAsync(MovieEntityIncludes));
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

        public async Task<bool> UpdateMovieAsync(int id, CreateMovieDTO createMovieDTO)
        {
            await CheckMovieDTO(createMovieDTO);
            
            Movie? movie = await GetMovieEntityByIdAsync(id);

            if (movie == null) return false;

            movie.MovieGenres.Clear();
            movie.MovieActors.Clear();

            _mapper.Map(createMovieDTO, movie);

            _unitOfWork.GetRepository<Movie>().Update(movie);
            await _unitOfWork.SaveAsync();

            return true;
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
            if (!string.IsNullOrEmpty(filter.SortBy) && !string.IsNullOrEmpty(filter.SortOrder))
            {
                bool ascending = filter.SortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase);
                movies = filter.SortBy.ToLower() switch
                {
                    "date" => ascending ? movies.OrderBy(m => m.ReleaseDate).ToList() : movies.OrderByDescending(m => m.ReleaseDate).ToList(),
                    "rating" => ascending ? movies.OrderBy(m => m.Rating).ToList() : movies.OrderByDescending(m => m.Rating).ToList(),
                    _ => movies.OrderByDescending(m => m.ReleaseDate).ToList()
                };
            }

            // Pagination
            int pageSize = 6;
            int totalMovies = movies.Count;
            int totalPages = (int)Math.Ceiling((double)totalMovies / pageSize);
            int pageNumber = filter.Page ?? 1;

            // Ensure the page number is within valid range
            if (pageNumber > totalPages) pageNumber = totalPages;
            if (pageNumber < 1) pageNumber = 1;

            movies = movies.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return movies.Select(m => _mapper.Map<MovieDTO>(m)).ToList();
        }
        private Task<Movie?> GetMovieEntityByIdAsync(int id) 
        {
            return _unitOfWork.GetRepository<Movie>().GetByIdAsync(id, MovieEntityIncludes);
        }
        private async Task CheckMovieDTO(CreateMovieDTO createMovieDTO) 
        {
            foreach(int actorId in createMovieDTO.ActorsIds) 
            {
                if (await _unitOfWork.GetRepository<Actor>().GetByIdAsync(actorId) == null)
                    throw new ArgumentException($"Actor with id {actorId} does not exist");
            }

            foreach(int genreId in createMovieDTO.GenresIds) 
            {
                if (await _unitOfWork.GetRepository<Genre>().GetByIdAsync(genreId) == null)
                    throw new ArgumentException($"Genre with id {genreId} does not exist");
            }
        }
        public async Task<List<MovieDTO>> GetSimilarMoviesAsync(int movieId)
        {
            var selectedMovie = await GetMovieEntityByIdAsync(movieId);
            if (selectedMovie == null || !selectedMovie.MovieGenres.Any())
            {
                return new List<MovieDTO>();
            }

            var allMovies = await _unitOfWork.GetRepository<Movie>().GetAllAsync(MovieEntityIncludes);
            var otherMovies = allMovies.Where(m => m.Id != movieId).ToList();

            var similarMovies = otherMovies
                .Select(movie => new
                {
                    Movie = movie,
                    CommonGenres = movie.MovieGenres.Select(g => g.GenreId)
                        .Intersect(selectedMovie.MovieGenres.Select(g => g.GenreId)).Count()
                })
                .Where(m => m.CommonGenres > 0)
                .OrderByDescending(m => m.CommonGenres)
                .Take(4)
                .Select(m => _mapper.Map<MovieDTO>(m.Movie))
                .ToList();

            return similarMovies;
        }

    }
}