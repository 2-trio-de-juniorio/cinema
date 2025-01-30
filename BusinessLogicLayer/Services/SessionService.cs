using BusinessLogic.Models.Sessions;
using BusinessLogicLayer.Interfaces;
using DataAccess.Models.Movies;
using DataAccess.Models.Sessions;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    // delete later
    internal sealed class SessionService//: ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMovieService _movieService;
/*
        public SessionService(IUnitOfWork unitOfWork, IMovieService movieService)
        {
            _unitOfWork = unitOfWork;
            _movieService = movieService;
        }

        public async Task<int> CreateSessionAsync(SessionDTO SessionDTO)
        {
            await ensureRelationsExistAsync(SessionDTO);

            Hall hall = (await _unitOfWork.GetRepository<Hall>().GetAllAsync("Seats"))
                .First(h => h.Name == SessionDTO.Hall.Name);

            hall.Name = SessionDTO.Hall.Name;
            hall.Seats.Clear();
            hall.Seats = SessionDTO.Hall.Seats
                .Select(seatDto => new Seat()
                    { IsBooked = seatDto.IsBooked, RowNumber = seatDto.RowNumber, SeatNumber = seatDto.SeatNumber })
                .ToList();

            Movie movie =
                (await _unitOfWork.GetRepository<Movie>().GetAllAsync("MovieGenres.Genre", "MovieActors.Actor"))
                .First(m => m.Title == SessionDTO.Movie.Title);

            Session session = new Session()
            {
                Hall = hall,
                Movie = movie,
                Price = SessionDTO.Price,
                StartTime = SessionDTO.StartTime,
            };
            await _unitOfWork.GetRepository<Session>().AddAsync(session);
            await _unitOfWork.SaveAsync();

            return session.Id;
        }

        public async Task<List<SessionDTO>> GetAllSessionsAsync()
        {
            return (await _unitOfWork.GetRepository<Session>()
                    .GetAllAsync("Movie.MovieActors.Actor", "Movie.MovieGenres.Genre", "Hall.Seats"))
                .Select(s => (SessionDTO)s)
                .ToList();
        }

        public async Task<SessionDTO?> GetSessionByIdAsync(int id)
        {
            Session? session = await _unitOfWork.GetRepository<Session>()
                .GetByIdAsync(id, "Movie.MovieActors.Actor", "Movie.MovieGenres.Genre", "Hall.Seats");
            return session != null ? (SessionDTO)session : null;
        }

        public async Task RemoveSessionAsync(int id)
        {
            await _unitOfWork.GetRepository<Session>().RemoveByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> UpdateSessionAsync(int id, SessionDTO SessionDTO)
        {
            Session? session = await _unitOfWork.GetRepository<Session>()
                .GetByIdAsync(id, "Movie.MovieActors.Actor", "Movie.MovieGenres.Genre", "Hall.Seats");

            if (session == null) return false;


            await ensureRelationsExistAsync(SessionDTO);

            Hall hall = (await _unitOfWork.GetRepository<Hall>().GetAllAsync("Seats"))
                .First(h => h.Name == SessionDTO.Hall.Name);

            hall.Name = SessionDTO.Hall.Name;
            hall.Seats.Clear();
            hall.Seats = SessionDTO.Hall.Seats
                .Select(seatDto => new Seat()
                    { IsBooked = seatDto.IsBooked, RowNumber = seatDto.RowNumber, SeatNumber = seatDto.SeatNumber })
                .ToList();

            session.Hall = hall;
            session.Movie = (await _unitOfWork.GetRepository<Movie>()
                    .GetAllAsync("MovieGenres.Genre", "MovieActors.Actor"))
                .First(m => m.Title == SessionDTO.Movie.Title);
            session.Price = SessionDTO.Price;
            session.StartTime = SessionDTO.StartTime;

            _unitOfWork.GetRepository<Session>().Update(session);
            await _unitOfWork.SaveAsync();

            return true;
        }

        private async Task ensureRelationsExistAsync(SessionDTO SessionDTO)
        {
            if (!(await _unitOfWork.GetRepository<Movie>().GetAllAsync()).Any(m => m.Title == SessionDTO.Movie.Title))
            {
                await _movieService.CreateMovieAsync(SessionDTO.Movie);
            }

            if (!(await _unitOfWork.GetRepository<Hall>().GetAllAsync()).Any(h => h.Name == SessionDTO.Hall.Name))
            {
                await _unitOfWork.GetRepository<Hall>().AddAsync(new Hall()
                {
                    Name = SessionDTO.Hall.Name,
                    Seats = SessionDTO.Hall.Seats.Select(seat => new Seat()
                            { IsBooked = seat.IsBooked, RowNumber = seat.RowNumber, SeatNumber = seat.SeatNumber })
                        .ToList()
                });
            }

            await _unitOfWork.SaveAsync();
        }*/
    }
}