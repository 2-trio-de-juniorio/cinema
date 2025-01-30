﻿using BusinessLogicLayer.Dtos;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebAPI.Controllers;

/// <summary>
/// API controller responsible for managing hall-related operations.
/// Provides CRUD endpoints for managing cinema halls.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class HallsController : ControllerBase
{
    private readonly IHallService _hallService;

    /// <summary>
    /// Initializes a new instance of the <see cref="HallsController"/> class.
    /// </summary>
    /// <param name="hallService">The service responsible for hall-related business logic.</param>
    public HallsController(IHallService hallService)
    {
        _hallService = hallService;
    }

    /// <summary>
    /// Retrieves a list of all halls.
    /// </summary>
    /// <returns>A list of <see cref="HallDto"/> objects representing all halls.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllHalls()
    {
        List<HallDto> halls = await _hallService.GetAllHallsAsync();
        return Ok(halls);
    }

    /// <summary>
    /// Retrieves details of a specific hall by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the hall.</param>
    /// <returns>A <see cref="HallDto"/> object representing the hall, or HTTP 404 if not found.</returns>
    [HttpGet("{id}", Name = "GetHallById")]
    public async Task<IActionResult> GetHallById([FromRoute] int id)
    {
        HallDto? hall = await _hallService.GetHallByIdAsync(id);

        if (hall == null)
        {
            return NotFound(new { Message = $"Hall with ID {id} not found." });
        }

        return Ok(hall);
    }

    /// <summary>
    /// Creates a new hall.
    /// </summary>
    /// <param name="hallDto">A <see cref="HallDto"/> object containing the details of the hall to create.</param>
    /// <returns>An HTTP 201 response if the hall is created successfully.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateHallAsync([FromBody] HallDto hallDto)
    {
        int id = await _hallService.CreateHallAsync(hallDto);
        return CreatedAtRoute(nameof(GetHallById), new { id }, hallDto);
    }

    /// <summary>
    /// Updates the details of an existing hall.
    /// </summary>
    /// <param name="id">The unique identifier of the hall to update.</param>
    /// <param name="hallDto">A <see cref="HallDto"/> object containing the updated details of the hall.</param>
    /// <returns>
    /// An HTTP 204 response if the hall was updated successfully, or HTTP 404 if the hall was not found.
    /// </returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHallAsync([FromRoute] int id, [FromBody] HallDto hallDto)
    {
        if (!await _hallService.UpdateHallAsync(id, hallDto))
        {
            return NotFound(new { Message = $"Hall with ID {id} not found." });
        }
        return NoContent();
    }

    /// <summary>
    /// Deletes a hall by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the hall to delete.</param>
    /// <returns>An HTTP 204 response if the hall was deleted successfully, or HTTP 404 if the hall was not found.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHall([FromRoute] int id)
    {
        if (!await _hallService.DeleteHallAsync(id))
        {
            return NotFound(new { Message = $"Hall with ID {id} not found." });
        }
        return NoContent();
    }
}
