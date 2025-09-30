namespace MarketAPI.Models.DTOs;

public class PaymentDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string? PaymentProvider { get; set; }
        public string? ProviderPaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTimeOffset? PaidAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }

    public class CreatePaymentDto
    {
        public int OrderId { get; set; }
        public string? PaymentProvider { get; set; }
        public string? ProviderPaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public string Status { get; set; } = "pending";
    }

    public class UpdatePaymentDto
    {
        public string Status { get; set; } = string.Empty;
        public DateTime? PaidAt { get; set; }
    }