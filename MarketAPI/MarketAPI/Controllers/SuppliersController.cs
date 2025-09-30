using MarketAPI.Models.DTOs;
using MarketAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> GetSuppliers()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDto>> GetSupplier(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            if (supplier == null) return NotFound($"Proveedor con ID {id} no encontrado");
            return Ok(supplier);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<SupplierDto>> GetSupplierByUser(int userId)
        {
            var supplier = await _supplierService.GetSupplierByUserIdAsync(userId);
            if (supplier == null) return NotFound($"Proveedor para usuario con ID {userId} no encontrado");
            return Ok(supplier);
        }

        [HttpPost]
        public async Task<ActionResult<SupplierDto>> CreateSupplier(CreateSupplierDto createSupplierDto)
        {
            try
            {
                var supplier = await _supplierService.CreateSupplierAsync(createSupplierDto);
                return CreatedAtAction(nameof(GetSupplier), new { id = supplier.Id }, supplier);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el proveedor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SupplierDto>> UpdateSupplier(int id, UpdateSupplierDto updateSupplierDto)
        {
            try
            {
                var supplier = await _supplierService.UpdateSupplierAsync(id, updateSupplierDto);
                if (supplier == null) return NotFound($"Proveedor con ID {id} no encontrado");
                return Ok(supplier);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el proveedor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSupplier(int id)
        {
            try
            {
                var result = await _supplierService.DeleteSupplierAsync(id);
                if (!result) return NotFound($"Proveedor con ID {id} no encontrado");
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/products")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetSupplierProducts(int id)
        {
            var products = await _supplierService.GetSupplierProductsAsync(id);
            return Ok(products);
        }

        [HttpGet("{id}/combos")]
        public async Task<ActionResult<IEnumerable<ComboDto>>> GetSupplierCombos(int id)
        {
            var combos = await _supplierService.GetSupplierCombosAsync(id);
            return Ok(combos);
        }
    }
}