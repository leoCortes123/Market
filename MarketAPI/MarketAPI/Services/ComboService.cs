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


public class ComboService : IComboService
{
    private readonly MarketDbContext _context;

    public ComboService(MarketDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ComboDto>> GetAllCombosAsync()
    {
        return await _context.Combos
            .Where(c => c.IsActive)
            .Include(c => c.ComboProducts)
                .ThenInclude(cp => cp.Product)
            .Include(c => c.ComboProducts)
                .ThenInclude(cp => cp.Unit)
            .Select(c => new ComboDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Price = c.Price,
                ImageUrl = c.ImageUrl,
                IsActive = c.IsActive,
                ComboProducts = c.ComboProducts.Select(cp => new ComboProductDetailDto
                {
                    ProductId = cp.ProductId,
                    ProductName = cp.Product.Name,
                    UnitId = cp.UnitId,
                    UnitName = cp.Unit.Name,
                    Quantity = cp.Quantity
                }).ToList()
            })
            .ToListAsync();
    }

    public async Task<ComboDto?> GetComboByIdAsync(int id)
    {
        var combo = await _context.Combos
            .Include(c => c.ComboProducts)
                .ThenInclude(cp => cp.Product)
            .Include(c => c.ComboProducts)
                .ThenInclude(cp => cp.Unit)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (combo == null) return null;

        return new ComboDto
        {
            Id = combo.Id,
            Name = combo.Name,
            Description = combo.Description,
            Price = combo.Price,
            ImageUrl = combo.ImageUrl,
            IsActive = combo.IsActive,
            ComboProducts = combo.ComboProducts.Select(cp => new ComboProductDetailDto
            {
                ProductId = cp.ProductId,
                ProductName = cp.Product.Name,
                UnitId = cp.UnitId,
                UnitName = cp.Unit.Name,
                Quantity = cp.Quantity
            }).ToList()
        };
    }

    public async Task<ComboDto> CreateComboAsync(CreateComboDto createComboDto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var combo = new Combo
            {
                Name = createComboDto.Name,
                Description = createComboDto.Description,
                Price = createComboDto.Price,
                ImageUrl = createComboDto.ImageUrl,
                IsActive = true
            };

            _context.Combos.Add(combo);
            await _context.SaveChangesAsync();

            // Agregar productos al combo
            foreach (var productDto in createComboDto.ComboProducts)
            {
                var comboProduct = new ComboProduct
                {
                    ComboId = combo.Id,
                    ProductId = productDto.ProductId,
                    UnitId = productDto.UnitId,
                    Quantity = productDto.Quantity
                };
                _context.ComboProducts.Add(comboProduct);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return await GetComboByIdAsync(combo.Id) ?? throw new Exception("Error al crear el combo");
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<ComboDto?> UpdateComboAsync(int id, UpdateComboDto updateComboDto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var combo = await _context.Combos
                .Include(c => c.ComboProducts)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (combo == null) return null;

            // Actualizar datos básicos del combo
            combo.Name = updateComboDto.Name;
            combo.Description = updateComboDto.Description;
            combo.Price = updateComboDto.Price;
            combo.ImageUrl = updateComboDto.ImageUrl;
            combo.IsActive = updateComboDto.IsActive;

            // Eliminar productos existentes
            _context.ComboProducts.RemoveRange(combo.ComboProducts);

            // Agregar nuevos productos
            foreach (var productDto in updateComboDto.ComboProducts)
            {
                var comboProduct = new ComboProduct
                {
                    ComboId = combo.Id,
                    ProductId = productDto.ProductId,
                    UnitId = productDto.UnitId,
                    Quantity = productDto.Quantity
                };
                _context.ComboProducts.Add(comboProduct);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return await GetComboByIdAsync(id);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> DeleteComboAsync(int id)
    {
        var combo = await _context.Combos
            .Include(c => c.ComboProducts)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (combo == null) return false;

        _context.ComboProducts.RemoveRange(combo.ComboProducts);
        _context.Combos.Remove(combo);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ToggleComboStatusAsync(int id)
    {
        var combo = await _context.Combos.FindAsync(id);
        if (combo == null) return false;

        combo.IsActive = !combo.IsActive;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<ComboProductDto>> GetComboProductsAsync(int comboId)
    {
        return await _context.ComboProducts
            .Where(cp => cp.ComboId == comboId)
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
}