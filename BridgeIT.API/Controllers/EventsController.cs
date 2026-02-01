using BridgeIT.API.DTOs.EventDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BridgeIT.Domain.Models;
using BridgeIT.Infrastructure;

namespace BridgeIT.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly BridgeItContext _dbContext;

    public EventsController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("add-event")]
    public async Task<IActionResult> AddEvent([FromBody] RegisterEventDTO dto)
    {

        if (dto.FacultyId == Guid.Empty)
        {
            return BadRequest("Faculty id can never be null");
        }

        var ev = new Event
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            SpeakerName = dto.SpeakerName,
            Description = dto.Description,
            Venue = dto.Venue,
            EventDate = dto.EventDate,
            FacultyId = dto.FacultyId
        };

        await _dbContext.Events.AddAsync(ev);
        await _dbContext.SaveChangesAsync();
        return Ok("Event Added Successfully!");
    }

    [HttpGet("get-events")]
    public async Task<IActionResult> GetAllEvents()
    {
        var events = await _dbContext.Events
            .Include(e => e.Faculty)
            .ToListAsync();

        if (!events.Any())
        {
            return NotFound("No events found.");
        }

        var list = events.Select(e => new GetEventDTO
        {
            Id = e.Id,
            Title = e.Title ?? string.Empty,
            Description = e.Description ?? string.Empty,
            SpeakerName = e.SpeakerName ?? string.Empty,
            EventDate = e.EventDate,
            Venue = e.Venue ?? string.Empty,
            FacultyId = e.FacultyId
        }).ToList();

        return(Ok(list));
    }

    [HttpGet("get-events-by-id/{id}")]
    public async Task<IActionResult> GetEventsById(Guid Id)
    {
        var events = await _dbContext.Events
            .Include(e => e.Faculty)
            .Where(e => e.FacultyId == Id)
            .ToListAsync();

        if (!events.Any())
        {
            return NotFound("No Events Found");
        }

        var list = events.Select(e => new GetEventDTO
        {
            Id = e.Id,
            Title = e.Title ?? string.Empty,
            Description = e.Description ?? string.Empty,
            SpeakerName = e.SpeakerName ?? string.Empty,
            EventDate = e.EventDate,
            Venue = e.Venue ?? string.Empty,
            FacultyId = e.FacultyId
        }).ToList();

        return Ok(list);

    }
    [HttpDelete("delete-event/{id}")]
    public async Task<IActionResult> DeleteEvents(Guid id)
    {
        var events = await _dbContext.Events.FindAsync(id);

        if (events == null)
        {
            return NotFound("Event not found.");
        }

        _dbContext.Events.Remove(events);
        await _dbContext.SaveChangesAsync();
        return Ok("Event deleted successfully.");
    }
}
