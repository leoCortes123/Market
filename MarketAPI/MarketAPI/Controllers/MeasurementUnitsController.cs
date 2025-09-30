using MarketAPI.Models.DTOs;
using MarketAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeasurementUnitsController : ControllerBase
    {
        private readonly IMeasurementUnitService _measurementUnitService;

        public MeasurementUnitsController(IMeasurementUnitService measurementUnitService)
        {
            _measurementUnitService = measurementUnitService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeasurementUnitDto>>> GetMeasurementUnits()
        {
            var units = await _measurementUnitService.GetAllMeasurementUnitsAsync();
            return Ok(units);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MeasurementUnitDto>> GetMeasurementUnit(int id)
        {
            var unit = await _measurementUnitService.GetMeasurementUnitByIdAsync(id);
            if (unit == null) return NotFound($"Unidad de medida con ID {id} no encontrada");
            return Ok(unit);
        }

        [HttpPost]
        public async Task<ActionResult<MeasurementUnitDto>> CreateMeasurementUnit(CreateMeasurementUnitDto createMeasurementUnitDto)
        {
            try
            {
                var unit = await _measurementUnitService.CreateMeasurementUnitAsync(createMeasurementUnitDto);
                return CreatedAtAction(nameof(GetMeasurementUnit), new { id = unit.Id }, unit);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear la unidad de medida: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MeasurementUnitDto>> UpdateMeasurementUnit(int id, UpdateMeasurementUnitDto updateMeasurementUnitDto)
        {
            try
            {
                var unit = await _measurementUnitService.UpdateMeasurementUnitAsync(id, updateMeasurementUnitDto);
                if (unit == null) return NotFound($"Unidad de medida con ID {id} no encontrada");
                return Ok(unit);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la unidad de medida: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMeasurementUnit(int id)
        {
            try
            {
                var result = await _measurementUnitService.DeleteMeasurementUnitAsync(id);
                if (!result) return NotFound($"Unidad de medida con ID {id} no encontrada");
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}