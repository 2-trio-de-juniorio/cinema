using BusinessLogic.Models.Tickets;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/tickets")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticketService"></param>
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        /// <summary>
        /// Retrieves all tickets.
        /// </summary>
        /// <returns>A list of all tickets.</returns>
        [HttpGet]
        public async Task<ActionResult<List<TicketDTO>>> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            return Ok(tickets);
        }

        /// <summary>
        /// Retrieves a ticket by its ID.
        /// </summary>
        /// <param name="id">The ID of the ticket.</param>
        /// <returns>The ticket details if found, or a 404 response if not.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDTO>> GetTicketById(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
                return NotFound($"Ticket with ID {id} not found.");

            return Ok(ticket);
        }

        /// <summary>
        /// Creates a new ticket.
        /// </summary>
        /// <param name="createTicketDto">The ticket data to be created.</param>
        /// <returns>The ID of the newly created ticket.</returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateTicket([FromBody] CreateTicketDTO createTicketDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            try
            {
                var ticketId = await _ticketService.CreateTicketAsync(createTicketDto);
                return CreatedAtAction(nameof(GetTicketById), new { id = ticketId }, ticketId);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing ticket.
        /// </summary>
        /// <param name="id">The ID of the ticket to update.</param>
        /// <param name="updateTicketDto">The updated ticket data.</param>
        /// <returns>A 204 response if the update is successful, or a 404 response if the ticket is not found.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTicket(int id, [FromBody] CreateTicketDTO updateTicketDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var success = await _ticketService.UpdateTicketAsync(id, updateTicketDto);
                if (!success)
                    return NotFound($"Ticket with ID {id} not found.");

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a ticket by its ID.
        /// </summary>
        /// <param name="id">The ID of the ticket to delete.</param>
        /// <returns>A 204 response if the deletion is successful, or a 404 response if the ticket is not found.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTicket(int id)
        {
            var success = await _ticketService.RemoveTicketAsync(id);
            if (!success)
                return NotFound($"Ticket with ID {id} not found.");

            return NoContent();
        }
    }
}
