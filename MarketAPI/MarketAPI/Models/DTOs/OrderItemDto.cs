namespace MarketAPI.Models.DTOs;

public class OrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int? SupplierProductId { get; set; }
        public decimal Quantity { get; set; }
        public int? UnitId { get; set; }
        public decimal Price { get; set; }
        public string? ProductName { get; set; }
        public string? UnitName { get; set; }
    }

    public class OrderItemCreateDto
    {
        public int SupplierProductId { get; set; }
        public decimal Quantity { get; set; }
        public int UnitId { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderComboItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int? SupplierComboId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? ComboName { get; set; }
    }

    public class OrderComboItemCreateDto
    {
        public int SupplierComboId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }