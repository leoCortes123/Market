using System;
using System.Collections.Generic;

namespace MarketAPI.Models;

public partial class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int? SupplierProductId { get; set; }

    public decimal Quantity { get; set; }

    public int? UnitId { get; set; }

    public decimal Price { get; set; }

    public int? MeasurementUnitId { get; set; }

    public virtual MeasurementUnit? MeasurementUnit { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual SupplierProduct? SupplierProduct { get; set; }

    public virtual MeasurementUnit? Unit { get; set; }
}
