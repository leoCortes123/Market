using MarketAPI.Data;
using MarketAPI.Models;
using MarketAPI.Models.DTOs;
using MarketAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarketAPI.Services;

public class CalendarService : ICalendarService
{
    private readonly MarketDbContext _context;

    public CalendarService(MarketDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CalendarDto>> GetAllCalendarsAsync()
    {
        return await _context.Calendars
            .Include(c => c.Events)
            .OrderBy(c => c.Date)
            .Select(c => new CalendarDto
            {
                Id = c.Id,
                Date = c.Date,
                Events = c.Events.Select(e => new EventDto
                {
                    Id = e.Id,
                    Place = e.Place,
                    Address = e.Address,
                    Description = e.Description,
                    TimeStart = e.TimeStart,
                    TimeEnd = e.TimeEnd,
                    CalendarId = e.CalendarId,
                    CalendarDate = c.Date.ToString("yyyy-MM-dd")
                }).ToList()
            })
            .ToListAsync();
    }

    public async Task<CalendarDto?> GetCalendarByIdAsync(int id)
    {
        var calendar = await _context.Calendars
            .Include(c => c.Events)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (calendar == null) return null;

        return new CalendarDto
        {
            Id = calendar.Id,
            Date = calendar.Date,
            Events = calendar.Events.Select(e => new EventDto
            {
                Id = e.Id,
                Place = e.Place,
                Address = e.Address,
                Description = e.Description,
                TimeStart = e.TimeStart,
                TimeEnd = e.TimeEnd,
                CalendarId = e.CalendarId,
                CalendarDate = calendar.Date.ToString("yyyy-MM-dd")
            }).ToList()
        };
    }

    public async Task<CalendarDto> CreateCalendarAsync(CreateCalendarDto createCalendarDto)
    {
        // Verificar si ya existe un calendario para esta fecha
        var existingCalendar = await _context.Calendars
            .FirstOrDefaultAsync(c => c.Date == createCalendarDto.Date);

        if (existingCalendar != null)
        {
            throw new InvalidOperationException("Ya existe un calendario para esta fecha");
        }

        var calendar = new Calendar
        {
            Date = createCalendarDto.Date
        };

        _context.Calendars.Add(calendar);
        await _context.SaveChangesAsync();

        return await GetCalendarByIdAsync(calendar.Id) ?? throw new Exception("Error al crear el calendario");
    }

    public async Task<CalendarDto?> UpdateCalendarAsync(int id, UpdateCalendarDto updateCalendarDto)
    {
        var calendar = await _context.Calendars.FindAsync(id);
        if (calendar == null) return null;

        // Verificar si la nueva fecha ya existe en otro calendario
        var existingCalendar = await _context.Calendars
            .FirstOrDefaultAsync(c => c.Date == updateCalendarDto.Date && c.Id != id);

        if (existingCalendar != null)
        {
            throw new InvalidOperationException("Ya existe otro calendario para esta fecha");
        }

        calendar.Date = updateCalendarDto.Date;
        await _context.SaveChangesAsync();

        return await GetCalendarByIdAsync(id);
    }

    public async Task<bool> DeleteCalendarAsync(int id)
    {
        var calendar = await _context.Calendars
            .Include(c => c.Events)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (calendar == null) return false;

        // Eliminar eventos relacionados primero
        _context.Events.RemoveRange(calendar.Events);
        _context.Calendars.Remove(calendar);

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<CalendarDto>> GetCalendarsByDateRangeAsync(DateOnly startDate, DateOnly endDate)
    {
        return await _context.Calendars
            .Include(c => c.Events)
            .Where(c => c.Date >= startDate && c.Date <= endDate)
            .OrderBy(c => c.Date)
            .Select(c => new CalendarDto
            {
                Id = c.Id,
                Date = c.Date,
                Events = c.Events.Select(e => new EventDto
                {
                    Id = e.Id,
                    Place = e.Place,
                    Address = e.Address,
                    Description = e.Description,
                    TimeStart = e.TimeStart,
                    TimeEnd = e.TimeEnd,
                    CalendarId = e.CalendarId,
                    CalendarDate = c.Date.ToString("yyyy-MM-dd")
                }).ToList()
            })
            .ToListAsync();
    }

    public async Task<CalendarDto?> GetCalendarByDateAsync(DateOnly date)
    {
        var calendar = await _context.Calendars
            .Include(c => c.Events)
            .FirstOrDefaultAsync(c => c.Date == date);

        if (calendar == null) return null;

        return new CalendarDto
        {
            Id = calendar.Id,
            Date = calendar.Date,
            Events = calendar.Events.Select(e => new EventDto
            {
                Id = e.Id,
                Place = e.Place,
                Address = e.Address,
                Description = e.Description,
                TimeStart = e.TimeStart,
                TimeEnd = e.TimeEnd,
                CalendarId = e.CalendarId,
                CalendarDate = calendar.Date.ToString("yyyy-MM-dd")
            }).ToList()
        };
    }
}