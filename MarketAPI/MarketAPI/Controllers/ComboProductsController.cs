using MarketAPI.Models.DTOs;
using MarketAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComboProductsController : ControllerBase
    {
        private readonly IComboProductService _comboProductService;

        public ComboProductsController(IComboProductService comboProductService)
        {
            _comboProductService = comboProductService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComboProductDto>>> GetComboProducts()
        {
            var comboProducts = await _comboProductService.GetAllComboProductsAsync();
            return Ok(comboProducts);
        }

        [HttpGet("combo/{comboId}/product/{productId}/unit/{unitId}")]
        public async Task<ActionResult<ComboProductDto>> GetComboProduct(int comboId, int productId, int unitId)
        {
            var comboProduct = await _comboProductService.GetComboProductAsync(comboId, productId, unitId);

            if (comboProduct == null)
            {
                return NotFound($"Relación Combo-Producto no encontrada");
            }

            return Ok(comboProduct);
        }

        [HttpPost("combo/{comboId}")]
        public async Task<ActionResult<ComboProductDto>> AddProductToCombo(int comboId, ComboProductItemDto comboProductDto)
        {
            try
            {
                var comboProduct = await _comboProductService.AddProductToComboAsync(comboProductDto, comboId);
                return CreatedAtAction(
                    nameof(GetComboProduct),
                    new { comboId = comboProduct.ComboId, productId = comboProduct.ProductId, unitId = comboProduct.UnitId },
                    comboProduct
                );
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al agregar producto al combo: {ex.Message}");
            }
        }

        [HttpPut("combo/{comboId}/product/{productId}/unit/{unitId}")]
        public async Task<ActionResult> UpdateProductInCombo(int comboId, int productId, int unitId, ComboProductItemDto comboProductDto)
        {
            var result = await _comboProductService.UpdateProductInComboAsync(comboId, productId, unitId, comboProductDto);

            if (!result)
            {
                return NotFound($"Relación Combo-Producto no encontrada");
            }

            return Ok(new { message = "Producto en combo actualizado correctamente" });
        }

        [HttpDelete("combo/{comboId}/product/{productId}/unit/{unitId}")]
        public async Task<ActionResult> RemoveProductFromCombo(int comboId, int productId, int unitId)
        {
            var result = await _comboProductService.RemoveProductFromComboAsync(comboId, productId, unitId);

            if (!result)
            {
                return NotFound($"Relación Combo-Producto no encontrada");
            }

            return NoContent();
        }
    }
}