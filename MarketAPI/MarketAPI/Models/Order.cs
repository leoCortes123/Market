using System;
using System.Collections.Generic;

namespace MarketAPI.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? SupplierId { get; set; }

    public string Status { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public string? PaymentMethod { get; set; }

    public string? PaymentStatus { get; set; }

    public string? ShippingAddress { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<OrderComboItem> OrderComboItems { get; set; } = new List<OrderComboItem>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Supplier? Supplier { get; set; }

    public virtual User? User { get; set; }
}
