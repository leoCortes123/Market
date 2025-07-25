using System;
using System.Collections.Generic;

namespace MarketAPI.Models;

public partial class SupplierCombo
{
    public int Id { get; set; }

    public int SupplierId { get; set; }

    public int ComboId { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Combo Combo { get; set; } = null!;

    public virtual ICollection<OrderComboItem> OrderComboItems { get; set; } = new List<OrderComboItem>();

    public virtual Supplier Supplier { get; set; } = null!;
}
