using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Models;

[Table("SupplierProducts")]
public class SupplierProduct
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int SupplierId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public decimal Price { get; set; }

    public int? UnitId { get; set; }

    [Required]
    public decimal Stock { get; set; }

    [Required]
    public bool IsActive { get; set; } = true;

    [Required]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public int? MeasurementUnitId { get; set; }

    // Navegación
    public Supplier Supplier { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public MeasurementUnit? Unit { get; set; }
    public MeasurementUnit? MeasurementUnit { get; set; }
}