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


public class CategoryService : ICategoryService
    {
        private readonly MarketDbContext _context;

        public CategoryService(MarketDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    ProductCount = c.Products.Count(p => p.IsActive)
                })
                .ToListAsync();
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ProductCount = category.Products.Count(p => p.IsActive)
            };
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var category = new Category
            {
                Name = createCategoryDto.Name,
                Description = createCategoryDto.Description
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return await GetCategoryByIdAsync(category.Id) ?? throw new Exception("Error al crear la categoría");
        }

        public async Task<CategoryDto?> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;

            category.Name = updateCategoryDto.Name;
            category.Description = updateCategoryDto.Description;

            await _context.SaveChangesAsync();

            return await GetCategoryByIdAsync(id);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return false;

            // Verificar si hay productos asociados
            if (category.Products.Any())
            {
                throw new InvalidOperationException("No se puede eliminar la categoría porque tiene productos asociados");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProductDto>> GetCategoryProductsAsync(int categoryId)
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
                    MeasurementUnitName = p.MeasurementUnit.Name
                })
                .ToListAsync();
        }
    }