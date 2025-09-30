using MarketAPI.Models;
using MarketAPI.Models.DTOs;


namespace MarketAPI.Services.Interfaces;

public interface IComboService
{
    Task<IEnumerable<ComboDto>> GetAllCombosAsync();
    Task<ComboDto?> GetComboByIdAsync(int id);
    Task<ComboDto> CreateComboAsync(CreateComboDto createComboDto);
    Task<ComboDto?> UpdateComboAsync(int id, UpdateComboDto updateComboDto);
    Task<bool> DeleteComboAsync(int id);
    Task<bool> ToggleComboStatusAsync(int id);
    Task<IEnumerable<ComboProductDto>> GetComboProductsAsync(int comboId);
}