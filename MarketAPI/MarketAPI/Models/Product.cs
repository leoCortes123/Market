using System;
using System.Collections.Generic;

namespace MarketAPI.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? DefaultUnitId { get; set; }

    public string? ImageUrl { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<ComboProduct> ComboProducts { get; set; } = new List<ComboProduct>();

    public virtual MeasurementUnit? DefaultUnit { get; set; }

    public virtual ICollection<SupplierProduct> SupplierProducts { get; set; } = new List<SupplierProduct>();
}
