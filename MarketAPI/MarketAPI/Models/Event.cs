using System.ComponentModel.DataAnnotations;

namespace MarketAPI.Models;

public class Event
{
    public int Id { get; set; }
    public string Place { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TimeOnly TimeStart { get; set; }
    public TimeOnly TimeEnd { get; set; }
    public int CalendarId { get; set; }

    // Navigation properties
    public Calendar Calendar { get; set; } = null!;
}
