using System;
using System.Collections.Generic;

namespace MarketAPI.Models;

public partial class SupplierProduct
{
    public int Id { get; set; }

    public int SupplierId { get; set; }

    public int ProductId { get; set; }

    public decimal Price { get; set; }

    public int? UnitId { get; set; }

    public decimal Stock { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? MeasurementUnitId { get; set; }

    public virtual MeasurementUnit? MeasurementUnit { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Product Product { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;

    public virtual MeasurementUnit? Unit { get; set; }
}
