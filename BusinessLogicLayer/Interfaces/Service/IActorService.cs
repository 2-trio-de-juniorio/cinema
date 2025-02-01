using BusinessLogic.Models.Movies;

namespace BusinessLogicLayer.Interfaces
{
    public interface IActorService
    {
        Task<List<ActorDTO>> GetAllActorsAsync();
        Task<ActorDTO?> GetActorByIdAsync(int id);
        Task<int> CreateActorAsync(CreateActorDTO actorDTO);
        Task<bool> UpdateActorAsync(int id, CreateActorDTO actorDTO);
        Task<bool> RemoveActorAsync(int id);
    }
}