using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Models;

[Table("Orders")]
public class Order
{
    [Key]
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? SupplierId { get; set; }

    [Required, StringLength(30)]
    public string Status { get; set; } = string.Empty;

    [Required]
    public decimal TotalAmount { get; set; }

    [StringLength(50)]
    public string? PaymentMethod { get; set; }

    [StringLength(20)]
    public string? PaymentStatus { get; set; }

    public string? ShippingAddress { get; set; }

    [Required]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    [Required]
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    // Navegación
    public User? User { get; set; }
    public Supplier? Supplier { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public ICollection<OrderComboItem> OrderComboItems { get; set; } = new List<OrderComboItem>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}