using MarketAPI.Models.DTOs;
using MarketAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDto>> GetPayment(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null) return NotFound($"Pago con ID {id} no encontrado");
            return Ok(payment);
        }

        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPaymentsByOrder(int orderId)
        {
            var payments = await _paymentService.GetPaymentsByOrderAsync(orderId);
            return Ok(payments);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDto>> CreatePayment(CreatePaymentDto createPaymentDto)
        {
            try
            {
                var payment = await _paymentService.CreatePaymentAsync(createPaymentDto);
                return CreatedAtAction(nameof(GetPayment), new { id = payment.Id }, payment);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el pago: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentDto>> UpdatePayment(int id, UpdatePaymentDto updatePaymentDto)
        {
            try
            {
                var payment = await _paymentService.UpdatePaymentAsync(id, updatePaymentDto);
                if (payment == null) return NotFound($"Pago con ID {id} no encontrado");
                return Ok(payment);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el pago: {ex.Message}");
            }
        }

        [HttpPatch("{id}/process")]
        public async Task<ActionResult> ProcessPayment(int id)
        {
            var result = await _paymentService.ProcessPaymentAsync(id);
            if (!result) return NotFound($"Pago con ID {id} no encontrado");
            return Ok(new { message = "Pago procesado correctamente" });
        }
    }
}