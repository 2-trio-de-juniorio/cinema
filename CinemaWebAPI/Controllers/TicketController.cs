﻿using BusinessLogic.Models.Tickets;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebAPI.Controllers
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
        [AllowAnonymous]
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
        [AllowAnonymous]
        public async Task<ActionResult<TicketDTO>> GetTicketById(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
                throw new KeyNotFoundException($"Ticket with ID {id} not found.");

            return Ok(ticket);
        }

        /// <summary>
        /// Creates a new ticket.
        /// </summary>
        /// <param name="createTicketDto">The ticket data to be created.</param>
        /// <returns>The ID of the newly created ticket.</returns>
        [HttpPost]
        // [Authorize(Policy = UserRole.Admin)]
        public async Task<ActionResult<int>> CreateTicket([FromBody] CreateTicketDTO createTicketDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var ticketId = await _ticketService.CreateTicketAsync(createTicketDto);
            return CreatedAtAction(nameof(GetTicketById), new { id = ticketId }, ticketId);
        }

        /// <summary>
        /// Updates an existing ticket.
        /// </summary>
        /// <param name="id">The ID of the ticket to update.</param>
        /// <param name="updateTicketDto">The updated ticket data.</param>
        /// <returns>A 204 response if the update is successful, or a 404 response if the ticket is not found.</returns>
        [HttpPut("{id}")]
        // [Authorize(Policy = UserRole.Admin)]
        public async Task<ActionResult> UpdateTicket(int id, [FromBody] CreateTicketDTO updateTicketDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _ticketService.UpdateTicketAsync(id, updateTicketDto);
            if (!success)
                throw new KeyNotFoundException($"Ticket with ID {id} not found.");

            return NoContent();
        }

        /// <summary>
        /// Deletes a ticket by its ID.
        /// </summary>
        /// <param name="id">The ID of the ticket to delete.</param>
        /// <returns>A 204 response if the deletion is successful, or a 404 response if the ticket is not found.</returns>
        [HttpDelete("{id}")]
        // [Authorize(Policy = UserRole.Admin)]
        public async Task<ActionResult> DeleteTicket(int id)
        {
            var success = await _ticketService.RemoveTicketAsync(id);
            if (!success)
                throw new KeyNotFoundException($"Ticket with ID {id} not found.");

            return NoContent();
        }
    }
}
