using MarketAPI.Custom;
using MarketAPI.Data;
using MarketAPI.Models;
using MarketAPI.Models.DTOs;
using MarketAPI.Models.DTOs.Auth;
using MarketAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace MarketAPI.Services;


public class ComboProductService : IComboProductService
{
    private readonly MarketDbContext _context;

    public ComboProductService(MarketDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ComboProductDto>> GetAllComboProductsAsync()
    {
        return await _context.ComboProducts
            .Include(cp => cp.Combo)
            .Include(cp => cp.Product)
            .Include(cp => cp.Unit)
            .Select(cp => new ComboProductDto
            {
                ComboId = cp.ComboId,
                ProductId = cp.ProductId,
                UnitId = cp.UnitId,
                Quantity = cp.Quantity,
                ComboName = cp.Combo.Name,
                ProductName = cp.Product.Name,
                UnitName = cp.Unit.Name
            })
            .ToListAsync();
    }

    public async Task<ComboProductDto?> GetComboProductAsync(int comboId, int productId, int unitId)
    {
        var comboProduct = await _context.ComboProducts
            .Include(cp => cp.Combo)
            .Include(cp => cp.Product)
            .Include(cp => cp.Unit)
            .FirstOrDefaultAsync(cp => cp.ComboId == comboId &&
                                     cp.ProductId == productId &&
                                     cp.UnitId == unitId);

        if (comboProduct == null) return null;

        return new ComboProductDto
        {
            ComboId = comboProduct.ComboId,
            ProductId = comboProduct.ProductId,
            UnitId = comboProduct.UnitId,
            Quantity = comboProduct.Quantity,
            ComboName = comboProduct.Combo.Name,
            ProductName = comboProduct.Product.Name,
            UnitName = comboProduct.Unit.Name
        };
    }

    public async Task<ComboProductDto> AddProductToComboAsync(ComboProductItemDto comboProductDto, int comboId)
    {
        // Verificar si ya existe
        var existing = await _context.ComboProducts
            .FirstOrDefaultAsync(cp => cp.ComboId == comboId &&
                                     cp.ProductId == comboProductDto.ProductId &&
                                     cp.UnitId == comboProductDto.UnitId);

        if (existing != null)
        {
            throw new InvalidOperationException("El producto ya existe en este combo con la misma unidad de medida");
        }

        var comboProduct = new ComboProduct
        {
            ComboId = comboId,
            ProductId = comboProductDto.ProductId,
            UnitId = comboProductDto.UnitId,
            Quantity = comboProductDto.Quantity
        };

        _context.ComboProducts.Add(comboProduct);
        await _context.SaveChangesAsync();

        return await GetComboProductAsync(comboId, comboProductDto.ProductId, comboProductDto.UnitId)
            ?? throw new Exception("Error al agregar producto al combo");
    }

    public async Task<bool> RemoveProductFromComboAsync(int comboId, int productId, int unitId)
    {
        var comboProduct = await _context.ComboProducts
            .FirstOrDefaultAsync(cp => cp.ComboId == comboId &&
                                     cp.ProductId == productId &&
                                     cp.UnitId == unitId);

        if (comboProduct == null) return false;

        _context.ComboProducts.Remove(comboProduct);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateProductInComboAsync(int comboId, int productId, int unitId, ComboProductItemDto comboProductDto)
    {
        var comboProduct = await _context.ComboProducts
            .FirstOrDefaultAsync(cp => cp.ComboId == comboId &&
                                     cp.ProductId == productId &&
                                     cp.UnitId == unitId);

        if (comboProduct == null) return false;

        comboProduct.Quantity = comboProductDto.Quantity;
        await _context.SaveChangesAsync();
        return true;
    }
}