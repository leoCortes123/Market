﻿using MarketAPI.Models.DTOs;


namespace MarketAPI.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto);
    Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto updateProductDto);
    Task<bool> DeleteProductAsync(int id);
    Task<bool> ToggleProductStatusAsync(int id);
    Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId);
}
