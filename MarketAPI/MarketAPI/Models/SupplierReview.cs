using System;
using System.Collections.Generic;

namespace MarketAPI.Models;

public partial class SupplierReview
{
    public int Id { get; set; }

    public int SupplierId { get; set; }

    public int? UserId { get; set; }

    public int Rating { get; set; }

    public string? Review { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Supplier Supplier { get; set; } = null!;

    public virtual User? User { get; set; }
}
