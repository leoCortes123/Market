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


public class ProductService : IProductService
{
    private readonly MarketDbContext _context;

    public ProductService(MarketDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        return await _context.Products
            .Where(p => p.IsActive)
            .Include(p => p.Category)
            .Include(p => p.MeasurementUnit)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                MeasurementUnitId = p.MeasurementUnitId,
                ImageUrl = p.ImageUrl,
                IsActive = p.IsActive,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                MeasurementUnitName = p.MeasurementUnit!.Name
            })
            .ToListAsync();
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.MeasurementUnit)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null) return null;

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            MeasurementUnitId = product.MeasurementUnitId,
            ImageUrl = product.ImageUrl,
            IsActive = product.IsActive,
            CategoryId = product.CategoryId,
            CategoryName = product.Category.Name,
            MeasurementUnitName = product.MeasurementUnit!.Name
        };
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
    {
        var product = new Product
        {
            Name = createProductDto.Name,
            Description = createProductDto.Description,
            MeasurementUnitId = createProductDto.MeasurementUnitId,
            ImageUrl = createProductDto.ImageUrl,
            CategoryId = createProductDto.CategoryId,
            IsActive = true
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return await GetProductByIdAsync(product.Id) ?? throw new Exception("Error al crear el producto");
    }

    public async Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return null;

        product.Name = updateProductDto.Name;
        product.Description = updateProductDto.Description;
        product.MeasurementUnitId = updateProductDto.MeasurementUnitId;
        product.ImageUrl = updateProductDto.ImageUrl;
        product.IsActive = updateProductDto.IsActive;
        product.CategoryId = updateProductDto.CategoryId;

        await _context.SaveChangesAsync();

        return await GetProductByIdAsync(id);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ToggleProductStatusAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        product.IsActive = !product.IsActive;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
    {
        return await _context.Products
            .Where(p => p.CategoryId == categoryId && p.IsActive)
            .Include(p => p.Category)
            .Include(p => p.MeasurementUnit)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                MeasurementUnitId = p.MeasurementUnitId,
                ImageUrl = p.ImageUrl,
                IsActive = p.IsActive,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                MeasurementUnitName = p.MeasurementUnit!.Name
            })
            .ToListAsync();
    }
}
