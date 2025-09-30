using MarketAPI.Models.DTOs;


namespace MarketAPI.Services.Interfaces;

public interface ISupplierService
    {
        Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync();
        Task<SupplierDto?> GetSupplierByIdAsync(int id);
        Task<SupplierDto?> GetSupplierByUserIdAsync(int userId);
        Task<SupplierDto> CreateSupplierAsync(CreateSupplierDto createSupplierDto);
        Task<SupplierDto?> UpdateSupplierAsync(int id, UpdateSupplierDto updateSupplierDto);
        Task<bool> DeleteSupplierAsync(int id);
        Task<IEnumerable<ProductDto>> GetSupplierProductsAsync(int supplierId);
        Task<IEnumerable<ComboDto>> GetSupplierCombosAsync(int supplierId);
    }