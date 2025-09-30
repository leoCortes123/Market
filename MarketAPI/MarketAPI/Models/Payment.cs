using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Models;

[Table("Payments")]
public class Payment
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int OrderId { get; set; }

    [StringLength(50)]
    public string? PaymentProvider { get; set; }

    [StringLength(100)]
    public string? ProviderPaymentId { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required, StringLength(10)]
    public string Currency { get; set; } = "USD";

    [Required, StringLength(30)]
    public string Status { get; set; } = string.Empty;

    public DateTimeOffset? PaidAt { get; set; }

    [Required]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    // Navegación
    public Order Order { get; set; } = null!;
}