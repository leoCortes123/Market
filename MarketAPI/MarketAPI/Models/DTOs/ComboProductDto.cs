namespace MarketAPI.Models.DTOs;

public class ComboProductDetailDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int UnitId { get; set; }
    public string UnitName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
}

public class ComboProductItemDto
{
    public int ProductId { get; set; }
    public int UnitId { get; set; }
    public decimal Quantity { get; set; }
}

public class ComboProductDto
{
    public int ComboId { get; set; }
    public int ProductId { get; set; }
    public int UnitId { get; set; }
    public decimal Quantity { get; set; }
    public string ComboName { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string UnitName { get; set; } = string.Empty;
}