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
            return (await _unitOfWork.GetRepository<Movie>().GetAllAsync("MovieActors.Actor", "MovieGenres.Genre"))
                .Select(m => _mapper.Map<MovieDTO>(m))
                .ToList();
        }

        public async Task<MovieDTO?> GetMovieByIdAsync(int id)
        {
            Movie? movie = await _unitOfWork.GetRepository<Movie>().GetByIdAsync(id, "MovieActors.Actor", "MovieGenres.Genre");
            return movie != null ? _mapper.Map<MovieDTO>(movie) : null;
        }

        public async Task RemoveMovieAsync(int id)
        {
            await _unitOfWork.GetRepository<Movie>().RemoveByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> UpdateMovieAsync(int id, CreateMovieDTO MovieDTO)
        {
            Movie? movie = await _unitOfWork.GetRepository<Movie>().GetByIdAsync(id, "MovieActors.Actor", "MovieGenres.Genre");

            if (movie == null) return false;

            movie.MovieGenres.Clear();
            movie.MovieActors.Clear();
            
            _mapper.Map(MovieDTO, movie);

            _unitOfWork.GetRepository<Movie>().Update(movie);
            await _unitOfWork.SaveAsync();

            return true;
        }
    }
}