using System;
using System.Collections.Generic;

namespace MarketAPI.Models;

public partial class ComboProduct
{
    public int ComboId { get; set; }

    public int ProductId { get; set; }

    public int UnitId { get; set; }

    public decimal Quantity { get; set; }

    public virtual Combo Combo { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual MeasurementUnit Unit { get; set; } = null!;
}
