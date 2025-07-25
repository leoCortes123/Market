using System;
using System.Collections.Generic;

namespace MarketAPI.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string? PaymentProvider { get; set; }

    public string? ProviderPaymentId { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime? PaidAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;
}
