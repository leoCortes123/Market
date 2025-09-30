using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Models;

[Table("OrderItems")]
public class OrderItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int OrderId { get; set; }

    public int? SupplierProductId { get; set; }

    [Required]
    public decimal Quantity { get; set; }

    public int? UnitId { get; set; }

    [Required]
    public decimal Price { get; set; }

    public int? MeasurementUnitId { get; set; }

    // Navegación
    public Order Order { get; set; } = null!;
    public SupplierProduct? SupplierProduct { get; set; }
    public MeasurementUnit? Unit { get; set; }
    public MeasurementUnit? MeasurementUnit { get; set; }
}