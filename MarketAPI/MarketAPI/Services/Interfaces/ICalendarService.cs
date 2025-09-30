using MarketAPI.Models.DTOs;

namespace MarketAPI.Services.Interfaces;

public interface ICalendarService
{
    Task<IEnumerable<CalendarDto>> GetAllCalendarsAsync();
    Task<CalendarDto?> GetCalendarByIdAsync(int id);
    Task<CalendarDto> CreateCalendarAsync(CreateCalendarDto createCalendarDto);
    Task<CalendarDto?> UpdateCalendarAsync(int id, UpdateCalendarDto updateCalendarDto);
    Task<bool> DeleteCalendarAsync(int id);
    Task<IEnumerable<CalendarDto>> GetCalendarsByDateRangeAsync(DateOnly startDate, DateOnly endDate);
    Task<CalendarDto?> GetCalendarByDateAsync(DateOnly date);
}