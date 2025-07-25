using System;
using System.Collections.Generic;

namespace MarketAPI.Models;

public partial class Combo
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<ComboProduct> ComboProducts { get; set; } = new List<ComboProduct>();

    public virtual ICollection<SupplierCombo> SupplierCombos { get; set; } = new List<SupplierCombo>();
}
