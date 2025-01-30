using BusinessLogicLayer.Dtos;
using BusinessLogicLayer.Interfaces;
using DataAccess.Models.Movies;
using DataAccess.Models.Sessions;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    // delete later
    internal sealed class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMovieService _movieService;
        public SessionService(IUnitOfWork unitOfWork, IMovieService movieService)
        {
            _unitOfWork = unitOfWork;
            _movieService = movieService;
        }

        public async Task<int> CreateSessionAsync(SessionDto sessionDto)
        {
            await ensureRelationsExistAsync(sessionDto);

            Hall hall = (await _unitOfWork.GetRepository<Hall>().GetAllAsync("Seats"))
                .First(h => h.Name == sessionDto.Hall.Name);

            hall.Name = sessionDto.Hall.Name;
            hall.Seats.Clear();
            hall.Seats = sessionDto.Hall.Seats
                .Select(seatDto => new Seat() { IsBooked = seatDto.IsBooked, RowNumber = seatDto.RowNumber, SeatNumber = seatDto.SeatNumber })
                .ToList();

            Movie movie =  (await _unitOfWork.GetRepository<Movie>().GetAllAsync("MovieGenres.Genre", "MovieActors.Actor"))
                .First(m => m.Title == sessionDto.Movie.Title);

            Session session = new Session()
            {
                Hall = hall,
                Movie = movie,
                Price = sessionDto.Price,
                StartTime = sessionDto.StartTime,
            };
            await _unitOfWork.GetRepository<Session>().AddAsync(session);
            await _unitOfWork.SaveAsync();

            return session.Id;
        }

        public async Task<List<SessionDto>> GetAllSessionsAsync()
        {
            return (await _unitOfWork.GetRepository<Session>().GetAllAsync("Movie.MovieActors.Actor", "Movie.MovieGenres.Genre", "Hall.Seats"))
                .Select(s => (SessionDto)s)
                .ToList();
        }

        public async Task<SessionDto?> GetSessionByIdAsync(int id)
        {
            Session? session = await _unitOfWork.GetRepository<Session>().GetByIdAsync(id, "Movie.MovieActors.Actor", "Movie.MovieGenres.Genre", "Hall.Seats");
            return session != null ? (SessionDto)session : null;
        }

        public async Task RemoveSessionAsync(int id)
        {
            await _unitOfWork.GetRepository<Session>().RemoveByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> UpdateSessionAsync(int id, SessionDto sessionDto)
        {
            Session? session = await _unitOfWork.GetRepository<Session>().GetByIdAsync(id, "Movie.MovieActors.Actor", "Movie.MovieGenres.Genre", "Hall.Seats");

            if (session == null) return false;


            await ensureRelationsExistAsync(sessionDto);

            Hall hall = (await _unitOfWork.GetRepository<Hall>().GetAllAsync("Seats"))
                .First(h => h.Name == sessionDto.Hall.Name);

            hall.Name = sessionDto.Hall.Name;
            hall.Seats.Clear();
            hall.Seats = sessionDto.Hall.Seats
                .Select(seatDto => new Seat() { IsBooked = seatDto.IsBooked, RowNumber = seatDto.RowNumber, SeatNumber = seatDto.SeatNumber })
                .ToList();

            session.Hall = hall;
            session.Movie = (await _unitOfWork.GetRepository<Movie>().GetAllAsync("MovieGenres.Genre", "MovieActors.Actor"))
                .First(m => m.Title == sessionDto.Movie.Title);
            session.Price = sessionDto.Price;
            session.StartTime = sessionDto.StartTime;

            _unitOfWork.GetRepository<Session>().Update(session);
            await _unitOfWork.SaveAsync();

            return true;
        }

        private async Task ensureRelationsExistAsync(SessionDto sessionDto)
        {
            if (!(await _unitOfWork.GetRepository<Movie>().GetAllAsync()).Any(m => m.Title == sessionDto.Movie.Title))
            {
                await _movieService.CreateMovieAsync(sessionDto.Movie);
            }

            if (!(await _unitOfWork.GetRepository<Hall>().GetAllAsync()).Any(h => h.Name == sessionDto.Hall.Name))
            {
                await _unitOfWork.GetRepository<Hall>().AddAsync(new Hall()
                {
                    Name = sessionDto.Hall.Name,
                    Seats = sessionDto.Hall.Seats.Select(seat => new Seat() { IsBooked = seat.IsBooked, RowNumber = seat.RowNumber, SeatNumber = seat.SeatNumber })
                        .ToList()
                });
            }

            await _unitOfWork.SaveAsync();
        }
    }
}