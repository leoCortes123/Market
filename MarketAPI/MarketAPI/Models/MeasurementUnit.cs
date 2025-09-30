
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Models;

[Table("MeasurementUnits")]
public class MeasurementUnit
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(10)]
    public string Abbreviation { get; set; } = string.Empty;

    [Required]
    public bool IsWeight { get; set; }

    public int? WeightInGrams { get; set; }

    public virtual ICollection<Product> Products { get; set; } = [];
}