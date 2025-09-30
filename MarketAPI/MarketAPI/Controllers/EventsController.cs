using MarketAPI.Models.DTOs;
using MarketAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetEvents()
    {
        var events = await _eventService.GetAllEventsAsync();
        return Ok(events);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EventDto>> GetEvent(int id)
    {
        var eventDto = await _eventService.GetEventByIdAsync(id);

        if (eventDto == null)
        {
            return NotFound($"Evento con ID {id} no encontrado");
        }

        return Ok(eventDto);
    }

    [HttpGet("calendar/{calendarId}")]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetEventsByCalendar(int calendarId)
    {
        var events = await _eventService.GetEventsByCalendarIdAsync(calendarId);
        return Ok(events);
    }

    [HttpGet("date/{date}")]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetEventsByDate(DateOnly date)
    {
        var events = await _eventService.GetEventsByDateAsync(date);
        return Ok(events);
    }

    [HttpGet("upcoming")]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetUpcomingEvents([FromQuery] int days = 30)
    {
        if (days <= 0 || days > 365)
        {
            return BadRequest("El número de días debe estar entre 1 y 365");
        }

        var events = await _eventService.GetUpcomingEventsAsync(days);
        return Ok(events);
    }

    [HttpPost]
    public async Task<ActionResult<EventDto>> CreateEvent(CreateEventDto createEventDto)
    {
        try
        {
            var eventDto = await _eventService.CreateEventAsync(createEventDto);
            return CreatedAtAction(nameof(GetEvent), new { id = eventDto.Id }, eventDto);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al crear el evento: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EventDto>> UpdateEvent(int id, UpdateEventDto updateEventDto)
    {
        try
        {
            var eventDto = await _eventService.UpdateEventAsync(id, updateEventDto);

            if (eventDto == null)
            {
                return NotFound($"Evento con ID {id} no encontrado");
            }

            return Ok(eventDto);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al actualizar el evento: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEvent(int id)
    {
        var result = await _eventService.DeleteEventAsync(id);

        if (!result)
        {
            return NotFound($"Evento con ID {id} no encontrado");
        }

        return NoContent();
    }
}