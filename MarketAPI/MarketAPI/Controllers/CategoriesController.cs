using MarketAPI.Models.DTOs;
using MarketAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound($"Categoría con ID {id} no encontrada");
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            try
            {
                var category = await _categoryService.CreateCategoryAsync(createCategoryDto);
                return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear la categoría: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                var category = await _categoryService.UpdateCategoryAsync(id, updateCategoryDto);
                if (category == null) return NotFound($"Categoría con ID {id} no encontrada");
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la categoría: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                var result = await _categoryService.DeleteCategoryAsync(id);
                if (!result) return NotFound($"Categoría con ID {id} no encontrada");
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/products")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetCategoryProducts(int id)
        {
            var products = await _categoryService.GetCategoryProductsAsync(id);
            return Ok(products);
        }
    }
}