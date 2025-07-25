using System;
using System.Collections.Generic;

namespace MarketAPI.Models;

public partial class OrderComboItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int? SupplierComboId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual SupplierCombo? SupplierCombo { get; set; }
}
