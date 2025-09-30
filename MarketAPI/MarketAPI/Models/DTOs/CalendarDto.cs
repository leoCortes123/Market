namespace MarketAPI.Models.DTOs;

public class CalendarDto
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public List<EventDto> Events { get; set; } = new();
}

public class CreateCalendarDto
{
    public DateOnly Date { get; set; }
}

public class UpdateCalendarDto
{
    public DateOnly Date { get; set; }
}