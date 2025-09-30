using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Models;

[Table("SupplierCombos")]
public class SupplierCombo
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int SupplierId { get; set; }

    [Required]
    public int ComboId { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Stock { get; set; }

    [Required]
    public bool IsActive { get; set; } = true;

    [Required]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    // Navegación
    public Supplier Supplier { get; set; } = null!;
    public Combo Combo { get; set; } = null!;
}