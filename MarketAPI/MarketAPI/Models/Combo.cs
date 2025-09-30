
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Models;

[Table("Combos")]
public class Combo
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }

    [Required]
    public bool IsActive { get; set; } = true;

    // Navegación
    public ICollection<ComboProduct> ComboProducts { get; set; } = new List<ComboProduct>();
    public ICollection<SupplierCombo> SupplierCombos { get; set; } = new List<SupplierCombo>();
}