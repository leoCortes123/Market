namespace MarketAPI.Models.DTOs;

public class OrderDto
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? SupplierId { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentStatus { get; set; }
        public string? ShippingAddress { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string? UserName { get; set; }
        public string? SupplierName { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new();
        public List<OrderComboItemDto> OrderComboItems { get; set; } = new();
        public List<PaymentDto> Payments { get; set; } = new();
    }

    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public int SupplierId { get; set; }
        public string? ShippingAddress { get; set; }
        public string? PaymentMethod { get; set; }
        public List<OrderItemCreateDto> OrderItems { get; set; } = new();
        public List<OrderComboItemCreateDto> OrderComboItems { get; set; } = new();
    }

    public class UpdateOrderDto
    {
        public string Status { get; set; } = string.Empty;
        public string? PaymentStatus { get; set; }
        public string? ShippingAddress { get; set; }
    }