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

        public async Task<int> CreateSessionAsync(CreateSessionDTO createSessionDto)
        {
            Session session = _mapper.Map<Session>(createSessionDto);

            await _unitOfWork.GetRepository<Session>().AddAsync(session);
            await _unitOfWork.SaveAsync();

            return session.Id;
        }

        public async Task<bool> UpdateSessionAsync(int id, CreateSessionDTO createSessionDto)
        {
            Session? session = await _unitOfWork.GetRepository<Session>().GetByIdAsync(id, SessionEntityIncludes);
            
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
    }
}