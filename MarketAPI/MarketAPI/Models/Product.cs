using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Models;

[Table("Products")]
public class Product
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int? MeasurementUnitId { get; set; }

    public string? ImageUrl { get; set; }

    [Required]
    public bool IsActive { get; set; } = true;

    [Required]
    public int CategoryId { get; set; }

    // Navegación
    public Category Category { get; set; } = null!;
    public MeasurementUnit? MeasurementUnit { get; set; }
}