using MarketAPI.Custom;
using MarketAPI.Data;
using MarketAPI.Models;
using MarketAPI.Models.DTOs;
using MarketAPI.Models.DTOs.Auth;
using MarketAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace MarketAPI.Services;


public class PaymentService : IPaymentService
    {
        private readonly MarketDbContext _context;

        public PaymentService(MarketDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync()
        {
            return await _context.Payments
                .Include(p => p.Order)
                .Select(p => new PaymentDto
                {
                    Id = p.Id,
                    OrderId = p.OrderId,
                    PaymentProvider = p.PaymentProvider,
                    ProviderPaymentId = p.ProviderPaymentId,
                    Amount = p.Amount,
                    Currency = p.Currency,
                    Status = p.Status,
                    PaidAt = p.PaidAt,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<PaymentDto?> GetPaymentByIdAsync(int id)
        {
            var payment = await _context.Payments
                .Include(p => p.Order)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (payment == null) return null;

            return new PaymentDto
            {
                Id = payment.Id,
                OrderId = payment.OrderId,
                PaymentProvider = payment.PaymentProvider,
                ProviderPaymentId = payment.ProviderPaymentId,
                Amount = payment.Amount,
                Currency = payment.Currency,
                Status = payment.Status,
                PaidAt = payment.PaidAt,
                CreatedAt = payment.CreatedAt
            };
        }

        public async Task<IEnumerable<PaymentDto>> GetPaymentsByOrderAsync(int orderId)
        {
            return await _context.Payments
                .Where(p => p.OrderId == orderId)
                .Include(p => p.Order)
                .Select(p => new PaymentDto
                {
                    Id = p.Id,
                    OrderId = p.OrderId,
                    PaymentProvider = p.PaymentProvider,
                    ProviderPaymentId = p.ProviderPaymentId,
                    Amount = p.Amount,
                    Currency = p.Currency,
                    Status = p.Status,
                    PaidAt = p.PaidAt,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto createPaymentDto)
        {
            var payment = new Payment
            {
                OrderId = createPaymentDto.OrderId,
                PaymentProvider = createPaymentDto.PaymentProvider,
                ProviderPaymentId = createPaymentDto.ProviderPaymentId,
                Amount = createPaymentDto.Amount,
                Currency = createPaymentDto.Currency,
                Status = createPaymentDto.Status,
                CreatedAt = DateTime.UtcNow
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return await GetPaymentByIdAsync(payment.Id) ?? throw new Exception("Error al crear el pago");
        }

        public async Task<PaymentDto?> UpdatePaymentAsync(int id, UpdatePaymentDto updatePaymentDto)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return null;

            payment.Status = updatePaymentDto.Status;
            payment.PaidAt = updatePaymentDto.PaidAt;

            // Si el pago se marca como completado, actualizar el estado de la orden
            if (updatePaymentDto.Status == "completed")
            {
                var order = await _context.Orders.FindAsync(payment.OrderId);
                if (order != null)
                {
                    order.PaymentStatus = "paid";
                    order.UpdatedAt = DateTime.UtcNow;
                }
            }

            await _context.SaveChangesAsync();

            return await GetPaymentByIdAsync(id);
        }

        public async Task<bool> ProcessPaymentAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return false;

            payment.Status = "completed";
            payment.PaidAt = DateTime.UtcNow;

            // Actualizar estado de la orden
            var order = await _context.Orders.FindAsync(payment.OrderId);
            if (order != null)
            {
                order.PaymentStatus = "paid";
                order.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }