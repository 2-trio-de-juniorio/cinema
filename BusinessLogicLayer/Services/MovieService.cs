using BusinessLogic.Models.Movies;
using BusinessLogicLayer.Interfaces;
using DataAccess.Models.Movies;
using DataAccess.Models.Movies.Actors;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    /*
    internal sealed class MovieService : IMovieService 
    {
        private readonly IUnitOfWork _unitOfWork;

        public MovieService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateMovieAsync(MovieDTO MovieDTO)
        {
            await ensureRelationsExistAsync(MovieDTO);
            var genres = (await _unitOfWork.GetRepository<Genre>()
                .GetAllAsync()).Where(g => MovieDTO.Genres.Any(genreDto => genreDto.Name == g.Name));
        
            var actors = (await _unitOfWork.GetRepository<Actor>()
                .GetAllAsync()).Where(a => MovieDTO.Actors.Any(actorDto => actorDto.ToString() == a.Firstname + " " + a.Lastname));

            Movie movie = new Movie()
            {
                Description = MovieDTO.Description,
                Duration = MovieDTO.Duration,
                PosterUrl = MovieDTO.PosterUrl,
                Rating = MovieDTO.Rating,
                ReleaseDate = MovieDTO.ReleaseDate,
                Title = MovieDTO.Title,
                TrailerUrl = MovieDTO.TrailerUrl,
                MovieGenres = genres.Select(genre => new MovieGenre() {Genre = genre}).ToList(),
                MovieActors = actors.Select(actor => new MovieActor() {Actor = actor}).ToList(),

            };

            await _unitOfWork.GetRepository<Movie>().AddAsync(movie);
            await _unitOfWork.SaveAsync();

            return movie.Id;
        }

        public async Task<List<MovieDTO>> GetAllMoviesAsync()
        {
            return (await _unitOfWork.GetRepository<Movie>().GetAllAsync("MovieActors.Actor", "MovieGenres.Genre"))
                .Select(m => (MovieDTO)m)
                .ToList();
        }

        public async Task<MovieDTO?> GetMovieByIdAsync(int id)
        {
            Movie? movie = await _unitOfWork.GetRepository<Movie>().GetByIdAsync(id, "MovieActors.Actor", "MovieGenres.Genre");
            return movie != null ? (MovieDTO)movie : null;
        }

        public async Task RemoveMovieAsync(int id)
        {
            await _unitOfWork.GetRepository<Movie>().RemoveByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> UpdateMovieAsync(int id, MovieDTO MovieDTO)
        {
            Movie? movie = await _unitOfWork.GetRepository<Movie>().GetByIdAsync(id, "MovieActors.Actor", "MovieGenres.Genre");

            if (movie == null) return false;

            movie.MovieGenres.Clear();
            movie.MovieActors.Clear();

            await ensureRelationsExistAsync(MovieDTO);

            var genres = (await _unitOfWork.GetRepository<Genre>()
                .GetAllAsync()).Where(g => MovieDTO.Genres.Any(genreDto => genreDto.Name == g.Name));
        
            var actors = (await _unitOfWork.GetRepository<Actor>()
                .GetAllAsync()).Where(a => MovieDTO.Actors.Any(actorDto => actorDto.ToString() == a.Firstname + " " + a.Lastname));

            movie.MovieGenres = genres.Select(genre => new MovieGenre() {Genre = genre}).ToList();
            movie.MovieActors = actors.Select(actor => new MovieActor() {Actor = actor}).ToList();

            movie.Description = MovieDTO.Description;
            movie.Duration = MovieDTO.Duration;
            movie.PosterUrl = MovieDTO.PosterUrl;
            movie.Rating = MovieDTO.Rating;
            movie.ReleaseDate = MovieDTO.ReleaseDate;
            movie.Title = MovieDTO.Title;
            movie.TrailerUrl = MovieDTO.TrailerUrl;

            _unitOfWork.GetRepository<Movie>().Update(movie);
            await _unitOfWork.SaveAsync();

            return true;
        }

        private async Task ensureRelationsExistAsync(MovieDTO MovieDTO)
        {
            Dictionary<string, GenreDto> genreList = new((
                    await _unitOfWork.GetRepository<Genre>().GetAllAsync())
                .Select(g => new KeyValuePair<string, GenreDto>(g.Name, new GenreDto() { Name = g.Name })));

            foreach (var genre in MovieDTO.Genres)
            {
                if (!genreList.ContainsKey(genre.Name))
                {
                    await _unitOfWork.GetRepository<Genre>().AddAsync(new Genre() { Name = genre.Name });
                }
            }

            Dictionary<string, ActorDto> actorList = new((
                    await _unitOfWork.GetRepository<Actor>().GetAllAsync())
                .Select(a => new KeyValuePair<string, ActorDto>(a.Firstname + " " + a.Lastname, new ActorDto() { Firstname = a.Firstname, Lastname = a.Lastname })));

            foreach (var actor in MovieDTO.Actors)
            {
                if (!actorList.ContainsKey(actor.Firstname + " " + actor.Lastname))
                {
                    await _unitOfWork.GetRepository<Actor>().AddAsync(new Actor() { Firstname = actor.Firstname, Lastname = actor.Lastname });
                }
            }
            await _unitOfWork.SaveAsync();
        }
    }*/
}