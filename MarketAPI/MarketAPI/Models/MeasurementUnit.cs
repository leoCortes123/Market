using System;
using System.Collections.Generic;

namespace MarketAPI.Models;

public partial class MeasurementUnit
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Abbreviation { get; set; } = null!;

    public bool IsWeight { get; set; }

    public int? WeightInGrams { get; set; }

    public virtual ICollection<ComboProduct> ComboProducts { get; set; } = new List<ComboProduct>();

    public virtual ICollection<OrderItem> OrderItemMeasurementUnits { get; set; } = new List<OrderItem>();

    public virtual ICollection<OrderItem> OrderItemUnits { get; set; } = new List<OrderItem>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<SupplierProduct> SupplierProductMeasurementUnits { get; set; } = new List<SupplierProduct>();

    public virtual ICollection<SupplierProduct> SupplierProductUnits { get; set; } = new List<SupplierProduct>();
}
