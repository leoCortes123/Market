using MarketAPI.Models.DTOs;
using MarketAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound($"Producto con ID {id} no encontrado");
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto createProductDto)
        {
            try
            {
                var product = await _productService.CreateProductAsync(createProductDto);
                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el producto: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> UpdateProduct(int id, UpdateProductDto updateProductDto)
        {
            try
            {
                var product = await _productService.UpdateProductAsync(id, updateProductDto);

                if (product == null)
                {
                    return NotFound($"Producto con ID {id} no encontrado");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el producto: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);

            if (!result)
            {
                return NotFound($"Producto con ID {id} no encontrado");
            }

            return NoContent();
        }

        [HttpPatch("{id}/toggle-status")]
        public async Task<ActionResult> ToggleProductStatus(int id)
        {
            var result = await _productService.ToggleProductStatusAsync(id);

            if (!result)
            {
                return NotFound($"Producto con ID {id} no encontrado");
            }

            return Ok(new { message = "Estado del producto actualizado correctamente" });
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }
    }
}