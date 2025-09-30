using MarketAPI.Data;
using MarketAPI.Models;
using MarketAPI.Models.DTOs;
using MarketAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarketAPI.Services;

public class EventService : IEventService
{
    private readonly MarketDbContext _context;

    public EventService(MarketDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
    {
        return await _context.Events
            .Include(e => e.Calendar)
            .OrderBy(e => e.Calendar.Date)
            .ThenBy(e => e.TimeStart)
            .Select(e => new EventDto
            {
                Id = e.Id,
                Place = e.Place,
                Address = e.Address,
                Description = e.Description,
                TimeStart = e.TimeStart,
                TimeEnd = e.TimeEnd,
                CalendarId = e.CalendarId,
                CalendarDate = e.Calendar.Date.ToString("yyyy-MM-dd")
            })
            .ToListAsync();
    }

    public async Task<EventDto?> GetEventByIdAsync(int id)
    {
        var eventEntity = await _context.Events
            .Include(e => e.Calendar)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (eventEntity == null) return null;

        return new EventDto
        {
            Id = eventEntity.Id,
            Place = eventEntity.Place,
            Address = eventEntity.Address,
            Description = eventEntity.Description,
            TimeStart = eventEntity.TimeStart,
            TimeEnd = eventEntity.TimeEnd,
            CalendarId = eventEntity.CalendarId,
            CalendarDate = eventEntity.Calendar.Date.ToString("yyyy-MM-dd")
        };
    }

    public async Task<EventDto> CreateEventAsync(CreateEventDto createEventDto)
    {
        // Verificar si el calendario existe
        var calendar = await _context.Calendars.FindAsync(createEventDto.CalendarId);
        if (calendar == null)
        {
            throw new InvalidOperationException("El calendario especificado no existe");
        }

        var eventEntity = new Event
        {
            Place = createEventDto.Place,
            Address = createEventDto.Address,
            Description = createEventDto.Description,
            TimeStart = createEventDto.TimeStart,
            TimeEnd = createEventDto.TimeEnd,
            CalendarId = createEventDto.CalendarId
        };

        _context.Events.Add(eventEntity);
        await _context.SaveChangesAsync();

        return await GetEventByIdAsync(eventEntity.Id) ?? throw new Exception("Error al crear el evento");
    }

    public async Task<EventDto?> UpdateEventAsync(int id, UpdateEventDto updateEventDto)
    {
        var eventEntity = await _context.Events.FindAsync(id);
        if (eventEntity == null) return null;

        // Verificar si el calendario existe
        var calendar = await _context.Calendars.FindAsync(updateEventDto.CalendarId);
        if (calendar == null)
        {
            throw new InvalidOperationException("El calendario especificado no existe");
        }

        eventEntity.Place = updateEventDto.Place;
        eventEntity.Address = updateEventDto.Address;
        eventEntity.Description = updateEventDto.Description;
        eventEntity.TimeStart = updateEventDto.TimeStart;
        eventEntity.TimeEnd = updateEventDto.TimeEnd;
        eventEntity.CalendarId = updateEventDto.CalendarId;

        await _context.SaveChangesAsync();

        return await GetEventByIdAsync(id);
    }

    public async Task<bool> DeleteEventAsync(int id)
    {
        var eventEntity = await _context.Events.FindAsync(id);
        if (eventEntity == null) return false;

        _context.Events.Remove(eventEntity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<EventDto>> GetEventsByCalendarIdAsync(int calendarId)
    {
        return await _context.Events
            .Include(e => e.Calendar)
            .Where(e => e.CalendarId == calendarId)
            .OrderBy(e => e.TimeStart)
            .Select(e => new EventDto
            {
                Id = e.Id,
                Place = e.Place,
                Address = e.Address,
                Description = e.Description,
                TimeStart = e.TimeStart,
                TimeEnd = e.TimeEnd,
                CalendarId = e.CalendarId,
                CalendarDate = e.Calendar.Date.ToString("yyyy-MM-dd")
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<EventDto>> GetEventsByDateAsync(DateOnly date)
    {
        return await _context.Events
            .Include(e => e.Calendar)
            .Where(e => e.Calendar.Date == date)
            .OrderBy(e => e.TimeStart)
            .Select(e => new EventDto
            {
                Id = e.Id,
                Place = e.Place,
                Address = e.Address,
                Description = e.Description,
                TimeStart = e.TimeStart,
                TimeEnd = e.TimeEnd,
                CalendarId = e.CalendarId,
                CalendarDate = e.Calendar.Date.ToString("yyyy-MM-dd")
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<EventDto>> GetUpcomingEventsAsync(int days = 30)
    {
        var startDate = DateOnly.FromDateTime(DateTime.Today);
        var endDate = startDate.AddDays(days);

        return await _context.Events
            .Include(e => e.Calendar)
            .Where(e => e.Calendar.Date >= startDate && e.Calendar.Date <= endDate)
            .OrderBy(e => e.Calendar.Date)
            .ThenBy(e => e.TimeStart)
            .Select(e => new EventDto
            {
                Id = e.Id,
                Place = e.Place,
                Address = e.Address,
                Description = e.Description,
                TimeStart = e.TimeStart,
                TimeEnd = e.TimeEnd,
                CalendarId = e.CalendarId,
                CalendarDate = e.Calendar.Date.ToString("yyyy-MM-dd")
            })
            .ToListAsync();
    }
}