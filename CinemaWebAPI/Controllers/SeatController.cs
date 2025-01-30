using BusinessLogic.Models.Sessions;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeatsController : ControllerBase
    {
        private readonly ISeatService _seatService;

        public SeatsController(ISeatService seatService)
        {
            _seatService = seatService;
        }

        /// <summary>
        /// Get a list of all seats.
        /// </summary>
        /// <returns>A list of seats</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<SeatDTO>), 200)]
        public async Task<IActionResult> GetAllSeats()
        {
            List<SeatDTO> seats = await _seatService.GetAllSeatsAsync();
            return Ok(seats);
        }

        /// <summary>
        /// Get a seat by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the seat</param>
        /// <returns>The seat details</returns>
        [HttpGet("{id}", Name = "GetSeatById")]
        [ProducesResponseType(typeof(SeatDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetSeatById([FromRoute] int id)
        {
            SeatDTO? seat = await _seatService.GetSeatByIdAsync(id);

            if (seat == null)
            {
                return NotFound(new { Message = $"Seat with ID {id} not found." });
            }

            return Ok(seat);
        }

        /// <summary>
        /// Create a new seat.
        /// </summary>
        /// <param name="seatDto">The seat data for creation</param>
        /// <returns>The created seat</returns>
        [HttpPost]
        [ProducesResponseType(typeof(SeatDTO), 201)]
        public async Task<IActionResult> CreateSeatAsync([FromBody] SeatDTO seatDto)
        {
            int id = await _seatService.CreateSeatAsync(seatDto);
            return CreatedAtRoute(nameof(GetSeatById), new { id }, seatDto);
        }

        /// <summary>
        /// Update a seat's details.
        /// </summary>
        /// <param name="id">The unique identifier of the seat to update</param>
        /// <param name="seatDto">The updated seat data</param>
        /// <returns>The result of the update operation</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateSeatAsync([FromRoute] int id, [FromBody] SeatDTO seatDto)
        {
            if (!await _seatService.UpdateSeatAsync(id, seatDto))
            {
                return NotFound(new { Message = $"Seat with ID {id} not found." });
            }
            return NoContent();
        }

        /// <summary>
        /// Delete a seat by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the seat to delete</param>
        /// <returns>The deletion status</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteSeatAsync([FromRoute] int id)
        {
            var seat = await _seatService.GetSeatByIdAsync(id);
            if (seat == null)
            {
                return NotFound(new { Message = $"Seat with ID {id} not found." });
            }

            await _seatService.RemoveSeatAsync(id);
            return NoContent();
        }
    }
}
