using AutoMapper;
using BusinessLogic.Models.Movies;
using BusinessLogic.Models.Sessions;
using BusinessLogicLayer.Interfaces;
using DataAccess.Models.Movies;
using DataAccess.Models.Movies.Actors;
using DataAccess.Models.Sessions;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            sessions = filter?.SortBy?.ToLower() switch
            {
                "price_asc" => sessions.OrderBy(m => m.Price).ToList(),
                "price_desc" => sessions.OrderByDescending(m => m.Price).ToList(),
                "Time_asc" => sessions.OrderBy(m => m.StartTime).ToList(),
                "Time_desc" => sessions.OrderByDescending(m => m.StartTime).ToList(),
                _ => sessions.OrderByDescending(m => m.Price).ToList()
            };

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

    }
}