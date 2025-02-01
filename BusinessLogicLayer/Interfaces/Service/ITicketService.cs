using BusinessLogic.Models.Tickets;

namespace BusinessLogicLayer.Interfaces
{
    public interface ITicketService
    {
        Task<TicketDTO?> GetTicketByIdAsync(int id);
        Task<List<TicketDTO>> GetAllTicketsAsync();
        Task<int> CreateTicketAsync(CreateTicketDTO ticketDto);
        Task<bool> UpdateTicketAsync(int id, CreateTicketDTO ticketDto);
        Task<bool> RemoveTicketAsync(int id);
    }
}