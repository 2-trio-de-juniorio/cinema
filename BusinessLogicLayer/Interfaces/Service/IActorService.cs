using BusinessLogic.Models.Movies;

namespace BusinessLogicLayer.Interfaces
{
    public interface IActorService
    {
        Task<List<ActorDTO>> GetAllActorsAsync();
        Task<ActorDTO?> GetActorByIdAsync(int id);
        Task<int> CreateActorAsync(CreateActorDTO createActorDTO);
        Task<bool> UpdateActorAsync(int id, CreateActorDTO createActorDTO);
        Task<bool> RemoveActorAsync(int id);
    }
}