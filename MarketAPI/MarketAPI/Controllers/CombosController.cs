using MarketAPI.Models.DTOs;
using MarketAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CombosController : ControllerBase
    {
        private readonly IComboService _comboService;

        public CombosController(IComboService comboService)
        {
            _comboService = comboService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComboDto>>> GetCombos()
        {
            var combos = await _comboService.GetAllCombosAsync();
            return Ok(combos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComboDto>> GetCombo(int id)
        {
            var combo = await _comboService.GetComboByIdAsync(id);

            if (combo == null)
            {
                return NotFound($"Combo con ID {id} no encontrado");
            }

            return Ok(combo);
        }

        [HttpPost]
        public async Task<ActionResult<ComboDto>> CreateCombo(CreateComboDto createComboDto)
        {
            try
            {
                var combo = await _comboService.CreateComboAsync(createComboDto);
                return CreatedAtAction(nameof(GetCombo), new { id = combo.Id }, combo);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el combo: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ComboDto>> UpdateCombo(int id, UpdateComboDto updateComboDto)
        {
            try
            {
                var combo = await _comboService.UpdateComboAsync(id, updateComboDto);

                if (combo == null)
                {
                    return NotFound($"Combo con ID {id} no encontrado");
                }

                return Ok(combo);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el combo: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCombo(int id)
        {
            var result = await _comboService.DeleteComboAsync(id);

            if (!result)
            {
                return NotFound($"Combo con ID {id} no encontrado");
            }

            return NoContent();
        }

        [HttpPatch("{id}/toggle-status")]
        public async Task<ActionResult> ToggleComboStatus(int id)
        {
            var result = await _comboService.ToggleComboStatusAsync(id);

            if (!result)
            {
                return NotFound($"Combo con ID {id} no encontrado");
            }

            return Ok(new { message = "Estado del combo actualizado correctamente" });
        }

        [HttpGet("{id}/products")]
        public async Task<ActionResult<IEnumerable<ComboProductDto>>> GetComboProducts(int id)
        {
            var comboProducts = await _comboService.GetComboProductsAsync(id);
            return Ok(comboProducts);
        }
    }
}