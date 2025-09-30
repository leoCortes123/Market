using MarketAPI.Models;
using MarketAPI.Models.DTOs;


namespace MarketAPI.Services.Interfaces;

public interface IComboProductService
{
    Task<IEnumerable<ComboProductDto>> GetAllComboProductsAsync();
    Task<ComboProductDto?> GetComboProductAsync(int comboId, int productId, int unitId);
    Task<ComboProductDto> AddProductToComboAsync(ComboProductItemDto comboProductDto, int comboId);
    Task<bool> RemoveProductFromComboAsync(int comboId, int productId, int unitId);
    Task<bool> UpdateProductInComboAsync(int comboId, int productId, int unitId, ComboProductItemDto comboProductDto);
}
