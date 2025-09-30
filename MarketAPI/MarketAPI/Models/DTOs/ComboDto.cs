namespace MarketAPI.Models.DTOs;

public class ComboDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public List<ComboProductDetailDto> ComboProducts { get; set; } = new();
}

public class CreateComboDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public List<ComboProductItemDto> ComboProducts { get; set; } = new();
}

public class UpdateComboDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public List<ComboProductItemDto> ComboProducts { get; set; } = new();
}