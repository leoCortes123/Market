using MarketAPI.Models.DTOs;
using MarketAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IComboService _comboService;

        public CatalogController(IProductService productService, IComboService comboService)
        {
            _productService = productService;
            _comboService = comboService;
        }

        [HttpGet("active-products")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetActiveProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("active-combos")]
        public async Task<ActionResult<IEnumerable<ComboDto>>> GetActiveCombos()
        {
            var combos = await _comboService.GetAllCombosAsync();
            return Ok(combos);
        }

        [HttpGet("category/{categoryId}/products")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }
    }
}