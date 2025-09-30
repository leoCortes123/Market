using MarketAPI.Models.DTOs;
using MarketAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound($"Orden con ID {id} no encontrada");
            return Ok(order);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByUser(int userId)
        {
            var orders = await _orderService.GetOrdersByUserAsync(userId);
            return Ok(orders);
        }

        [HttpGet("supplier/{supplierId}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersBySupplier(int supplierId)
        {
            var orders = await _orderService.GetOrdersBySupplierAsync(supplierId);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderDto createOrderDto)
        {
            try
            {
                var order = await _orderService.CreateOrderAsync(createOrderDto);
                return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear la orden: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDto>> UpdateOrder(int id, UpdateOrderDto updateOrderDto)
        {
            try
            {
                var order = await _orderService.UpdateOrderAsync(id, updateOrderDto);
                if (order == null) return NotFound($"Orden con ID {id} no encontrada");
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la orden: {ex.Message}");
            }
        }

        [HttpPatch("{id}/cancel")]
        public async Task<ActionResult> CancelOrder(int id)
        {
            try
            {
                var result = await _orderService.CancelOrderAsync(id);
                if (!result) return NotFound($"Orden con ID {id} no encontrada");
                return Ok(new { message = "Orden cancelada correctamente" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrderAsync(id);
            if (!result) return NotFound($"Orden con ID {id} no encontrada");
            return NoContent();
        }
    }
}