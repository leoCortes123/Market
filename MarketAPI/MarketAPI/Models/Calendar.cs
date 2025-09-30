using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace MarketAPI.Models;

public class Calendar
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }

    // Navigation properties
    public ICollection<Event> Events { get; set; } = new List<Event>();
}
