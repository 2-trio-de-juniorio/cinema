using BusinessLogic.Models.Tickets;

namespace BusinessLogicLayer.Interfaces
{
    public interface ITicketService
    {
        Task<int> CreateTicketAsync(CreateTicketDTO createTicketDTO);
        Task<TicketDTO?> GetTicketByIdAsync(int id);
        Task<List<TicketDTO>> GetAllTicketsAsync();
        Task<bool> UpdateTicketAsync(int id, CreateTicketDTO createTicketDTO);
        Task<bool> RemoveTicketAsync(int id);
    }
}