namespace MarketAPI.Models.DTOs;

public class EventDto
{
    public int Id { get; set; }
    public string Place { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TimeOnly TimeStart { get; set; }
    public TimeOnly TimeEnd { get; set; }
    public int CalendarId { get; set; }
    public string CalendarDate { get; set; } = string.Empty;
}

public class CreateEventDto
{
    public string Place { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TimeOnly TimeStart { get; set; }
    public TimeOnly TimeEnd { get; set; }
    public int CalendarId { get; set; }
}

public class UpdateEventDto
{
    public string Place { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TimeOnly TimeStart { get; set; }
    public TimeOnly TimeEnd { get; set; }
    public int CalendarId { get; set; }
}