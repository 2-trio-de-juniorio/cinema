using BusinessLogic.Models.Tickets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface ITicketService
    {
        Task<int> CreateTicketAsync(CreateTicketDTO ticketDto);
        Task<TicketDTO?> GetTicketByIdAsync(int id);
        Task<List<TicketDTO>> GetAllTicketsAsync();
        Task<bool> UpdateTicketAsync(int id, CreateTicketDTO ticketDto);
        Task<bool> RemoveTicketAsync(int id);
    }
}