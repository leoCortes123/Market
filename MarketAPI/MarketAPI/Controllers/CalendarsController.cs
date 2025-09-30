using MarketAPI.Models.DTOs;
using MarketAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalendarsController : ControllerBase
{
    private readonly ICalendarService _calendarService;

    public CalendarsController(ICalendarService calendarService)
    {
        _calendarService = calendarService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CalendarDto>>> GetCalendars()
    {
        var calendars = await _calendarService.GetAllCalendarsAsync();
        return Ok(calendars);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CalendarDto>> GetCalendar(int id)
    {
        var calendar = await _calendarService.GetCalendarByIdAsync(id);

        if (calendar == null)
        {
            return NotFound($"Calendario con ID {id} no encontrado");
        }

        return Ok(calendar);
    }

    [HttpGet("date/{date}")]
    public async Task<ActionResult<CalendarDto>> GetCalendarByDate(DateOnly date)
    {
        var calendar = await _calendarService.GetCalendarByDateAsync(date);

        if (calendar == null)
        {
            return NotFound($"No hay calendario para la fecha {date:yyyy-MM-dd}");
        }

        return Ok(calendar);
    }

    [HttpGet("range")]
    public async Task<ActionResult<IEnumerable<CalendarDto>>> GetCalendarsByDateRange(
        [FromQuery] DateOnly startDate,
        [FromQuery] DateOnly endDate)
    {
        if (startDate > endDate)
        {
            return BadRequest("La fecha de inicio no puede ser mayor que la fecha de fin");
        }

        var calendars = await _calendarService.GetCalendarsByDateRangeAsync(startDate, endDate);
        return Ok(calendars);
    }

    [HttpPost]
    public async Task<ActionResult<CalendarDto>> CreateCalendar(CreateCalendarDto createCalendarDto)
    {
        try
        {
            var calendar = await _calendarService.CreateCalendarAsync(createCalendarDto);
            return CreatedAtAction(nameof(GetCalendar), new { id = calendar.Id }, calendar);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al crear el calendario: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CalendarDto>> UpdateCalendar(int id, UpdateCalendarDto updateCalendarDto)
    {
        try
        {
            var calendar = await _calendarService.UpdateCalendarAsync(id, updateCalendarDto);

            if (calendar == null)
            {
                return NotFound($"Calendario con ID {id} no encontrado");
            }

            return Ok(calendar);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al actualizar el calendario: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCalendar(int id)
    {
        var result = await _calendarService.DeleteCalendarAsync(id);

        if (!result)
        {
            return NotFound($"Calendario con ID {id} no encontrado");
        }

        return NoContent();
    }
}