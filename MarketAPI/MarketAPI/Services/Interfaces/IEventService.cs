using MarketAPI.Models.DTOs;

namespace MarketAPI.Services.Interfaces;

public interface IEventService
{
    Task<IEnumerable<EventDto>> GetAllEventsAsync();
    Task<EventDto?> GetEventByIdAsync(int id);
    Task<EventDto> CreateEventAsync(CreateEventDto createEventDto);
    Task<EventDto?> UpdateEventAsync(int id, UpdateEventDto updateEventDto);
    Task<bool> DeleteEventAsync(int id);
    Task<IEnumerable<EventDto>> GetEventsByCalendarIdAsync(int calendarId);
    Task<IEnumerable<EventDto>> GetEventsByDateAsync(DateOnly date);
    Task<IEnumerable<EventDto>> GetUpcomingEventsAsync(int days = 30);
}