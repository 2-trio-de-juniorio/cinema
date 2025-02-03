using AutoMapper;
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
            return _mapper.Map<List<Session>, List<SessionDTO>>(
                await _unitOfWork.GetRepository<Session>().GetAllAsync(SessionEntityIncludes));

        }

        public async Task<SessionDTO?> GetSessionByIdAsync(int id)
        {
            Session? session = await _unitOfWork.GetRepository<Session>().GetByIdAsync(id, SessionEntityIncludes);
            
            return session != null ? _mapper.Map<SessionDTO>(session) : null;
        }

        public async Task<int> CreateSessionAsync(CreateSessionDTO createSessionDTO)
        {
            await checkSessionDTO(createSessionDTO);

            Session session = _mapper.Map<Session>(createSessionDTO);

            await _unitOfWork.GetRepository<Session>().AddAsync(session);
            await _unitOfWork.SaveAsync();

            return session.Id;
        }

        public async Task<bool> UpdateSessionAsync(int id, CreateSessionDTO createSessionDTO)
        {
            Session? session = await _unitOfWork.GetRepository<Session>().GetByIdAsync(id, SessionEntityIncludes);
            
            if (session == null) return false;
            
            await checkSessionDTO(createSessionDTO);

            _mapper.Map(createSessionDTO, session);

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

            // Ensure the page number is within valid range
            if (pageNumber > totalPages) pageNumber = totalPages;
            if (pageNumber < 1) pageNumber = 1;

            sessions = sessions.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return sessions.Select(m => _mapper.Map<SessionDTO>(m)).ToList();
        }

        private async Task checkSessionDTO(CreateSessionDTO createSessionDTO) 
        {
            Hall? hall = await _unitOfWork.GetRepository<Hall>().GetByIdAsync(createSessionDTO.HallId);

            if (hall == null)
                throw new ArgumentException($"Hall with id {createSessionDTO.HallId} does not exist");

            Movie? movie = await _unitOfWork.GetRepository<Movie>().GetByIdAsync(createSessionDTO.MovieId);

            if (movie == null)
                throw new ArgumentException($"Movie with id {createSessionDTO.MovieId} does not exist");
        }
    }
}