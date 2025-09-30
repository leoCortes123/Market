using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Models;

[Table("ComboProducts")]
public class ComboProduct
{
    [Column("ComboId")]
    public int ComboId { get; set; }

    [Column("ProductId")]
    public int ProductId { get; set; }

    [Column("UnitId")]
    public int UnitId { get; set; }

    [Column("Quantity"), Required]
    public decimal Quantity { get; set; }

    // Navegación
    public Combo Combo { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public MeasurementUnit Unit { get; set; } = null!;
}