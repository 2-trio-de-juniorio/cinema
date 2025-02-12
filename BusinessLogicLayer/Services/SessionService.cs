using AutoMapper;
using BusinessLogic.Models.Movies;
using BusinessLogic.Models.Sessions;
using BusinessLogicLayer.Interfaces;
using DataAccess.Models.Movies;
using DataAccess.Models.Movies.Actors;
using DataAccess.Models.Sessions;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    internal sealed class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly string[] SessionEntityIncludes =
        [
            nameof(Session.Movie), 
            nameof(Session.Hall),
            $"{nameof(Session.Movie)}.{nameof(Movie.MovieActors)}.{nameof(MovieActor.Actor)}", 
            $"{nameof(Session.Movie)}.{nameof(Movie.MovieGenres)}.{nameof(MovieGenre.Genre)}"
        ];//this is more save way, but also more 

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<SessionDTO>> GetAllSessionsAsync()
        {
            var result = await _unitOfWork.GetRepository<Session>().GetAllAsync(SessionEntityIncludes);
            return result.Select(m => _mapper.Map<SessionDTO>(m)).ToList();
        }

        public async Task<SessionDTO?> GetSessionByIdAsync(int id)
        {
            var session = await GetSessionEntityByIdAsync(id);
            return session == null ? null : _mapper.Map<SessionDTO>(session);
        }

        public async Task<int> CreateSessionAsync(CreateSessionDTO createSessionDto)
        {
            Session session = _mapper.Map<Session>(createSessionDto);
            await _unitOfWork.GetRepository<Session>().AddAsync(session);
            await _unitOfWork.SaveAsync();
            return session.Id;
        }

        public async Task<bool> UpdateSessionAsync(int id, CreateSessionDTO createSessionDto)
        {
            Session? session = await GetSessionEntityByIdAsync(id);
            
            if (session == null) return false;
            
            _mapper.Map(createSessionDto, session);

            _unitOfWork.GetRepository<Session>().Update(session);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> RemoveSessionAsync(int id)
        {
            bool success = await _unitOfWork.GetRepository<Session>().RemoveByIdAsync(id);
            await _unitOfWork.SaveAsync();
            return success;
        }

        private async Task<Session?> GetSessionEntityByIdAsync(int id)
        {
            return await _unitOfWork.GetRepository<Session>().GetByIdAsync(id, SessionEntityIncludes);
        }
        
        public async Task<List<SessionDTO>> GetFilteredSessionsAsync(SessionFilterDTO filter)
        {
            var sessions = await _unitOfWork.GetRepository<Session>().GetAllAsync(SessionEntityIncludes);

            if (filter?.Date.HasValue == true)
            {
                sessions = sessions.Where(s => s.StartTime.Date == filter.Date.Value.Date).ToList();
            }

            // Sorting
            if (!string.IsNullOrEmpty(filter?.SortBy) && !string.IsNullOrEmpty(filter?.SortOrder))
            {
                bool ascending = filter.SortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase);
                sessions = filter.SortBy.ToLower() switch
                {
                    "price" => ascending ? sessions.OrderBy(m => m.Price).ToList() : sessions.OrderByDescending(m => m.Price).ToList(),
                    "time" => ascending ? sessions.OrderBy(m => m.StartTime).ToList() : sessions.OrderByDescending(m => m.StartTime).ToList(),
                    _ => sessions.OrderByDescending(m => m.StartTime).ToList()
                };
            }

            // Pagination
            int pageSize = 6;
            int totalSessions = sessions.Count;
            int totalPages = (int)Math.Ceiling((double)totalSessions / pageSize);
            int pageNumber = filter?.Page ?? 1;

            // Valid range
            if (pageNumber > totalPages) pageNumber = totalPages;
            if (pageNumber < 1) pageNumber = 1;

            sessions = sessions.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return sessions.Select(m => _mapper.Map<SessionDTO>(m)).ToList();
        }

        public async Task<SessionsByMovieDTO> GetMoviesWithSessionsAsync(DateTime? date, string? genre, int page = 1, int pageSize = 6)
        {
            var sessions = await _unitOfWork.GetRepository<Session>().GetAllAsync(SessionEntityIncludes);

            if (date.HasValue)
            {
                sessions = sessions.Where(s => s.StartTime.Date == date.Value.Date).ToList();
            }

            if (!string.IsNullOrEmpty(genre))
            {
                sessions = sessions.Where(s => s.Movie.MovieGenres.Any(g => g.Genre.Name == genre)).ToList();
            }

            var groupedSessions = sessions
                .GroupBy(s => s.Movie)
                .Select(g => new SessionByFilmDTO
                {
                    Movie = _mapper.Map<MoviePreviewDTO>(g.Key),
                    Sessions = g.Select(s => new SessionPreviewDTO
                    {
                        Id = s.Id,
                        Price = (decimal)s.Price,
                        DateTime = s.StartTime
                    }).ToList()
                }).ToList();

            int totalSessions = groupedSessions.Count;
            int totalPages = (int)Math.Ceiling((double)totalSessions / pageSize);
            if (page > totalPages) page = totalPages;
            if (page < 1) page = 1;

            groupedSessions = groupedSessions.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new SessionsByMovieDTO
            {
                SessionsGroupedByFilms = groupedSessions,
                CurrentPage = page,
                MaxPage = totalPages
            };

        }

        public async Task<MovieSessionsDTO?> GetSessionsByMovieAsync(int movieId, DateTime? date)
        {
            var sessions = await _unitOfWork.GetRepository<Session>().GetAllAsync(SessionEntityIncludes);

            var filteredSessions = sessions.Where(s => s.Movie.Id == movieId);

            if (date.HasValue)
            {
                filteredSessions = filteredSessions.Where(s => s.StartTime.Date == date.Value.Date);
            }

            var movie = sessions.Select(s => s.Movie).FirstOrDefault(m => m.Id == movieId);
            if (movie == null) return null;

            var sessionList = filteredSessions.Select(s => new SessionPreviewDTO
            {
                Id = s.Id,
                Price = (decimal)s.Price,
                DateTime = s.StartTime
            }).ToList();

            return new MovieSessionsDTO
            {
                Movie = _mapper.Map<MoviePreviewDTO>(movie),
                Sessions = sessionList
            };
        }

    }
}