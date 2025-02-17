using AutoMapper;
using BusinessLogic.Models.Movies;
using BusinessLogicLayer.Interfaces;
using DataAccess.Models.Movies.Actors;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    public class ActorService : IActorService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ActorService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ActorDTO>> GetAllActorsAsync()
        {
            return _mapper.Map<List<Actor>, List<ActorDTO>>(
                await _unitOfWork.GetRepository<Actor>().GetAllAsync());
        }

        public async Task<ActorDTO?> GetActorByIdAsync(int id)
        {
            Actor? actor = await _unitOfWork.GetRepository<Actor>().GetByIdAsync(id);
            return actor != null ? _mapper.Map<ActorDTO>(actor) : null;
        }

        public async Task<int> CreateActorAsync(CreateActorDTO actorDTO)
        {
            Actor actor = _mapper.Map<Actor>(actorDTO);

            await _unitOfWork.GetRepository<Actor>().AddAsync(actor);
            await _unitOfWork.SaveAsync();

            return actor.Id;
        }

        public async Task<bool> UpdateActorAsync(int id, CreateActorDTO actorDTO)
        {
            Actor? actor = await _unitOfWork.GetRepository<Actor>().GetByIdAsync(id);

            if (actor == null) return false;

            _mapper.Map(actorDTO, actor);

            _unitOfWork.GetRepository<Actor>().Update(actor);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> RemoveActorAsync(int id)
        {
            bool result = await _unitOfWork.GetRepository<Actor>().RemoveByIdAsync(id);
            await _unitOfWork.SaveAsync();

            return result;
        }
    }
}