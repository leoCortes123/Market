using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Models;

[Table("OrderComboItems")]
public class OrderComboItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int OrderId { get; set; }

    public int? SupplierComboId { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal Price { get; set; }

    // Navegación
    public Order Order { get; set; } = null!;
    public SupplierCombo? SupplierCombo { get; set; }
}