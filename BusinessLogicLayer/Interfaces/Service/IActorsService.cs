using BusinessLogic.Models.Movies;

namespace BusinessLogicLayer.Interfaces
{
    public interface IActorsService
    {
        Task<List<ActorDTO>> GetAllActorsAsync();
        Task<ActorDTO?> GetActorByIdAsync(int id);
        Task<int> CreateActorAsync(CreateActorDTO movieDto);
        Task<bool> UpdateActorAsync(int id, CreateActorDTO movieDto);
        Task<bool> RemoveActorAsync(int id);
    }
}