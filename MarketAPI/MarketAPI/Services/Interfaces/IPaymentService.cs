using MarketAPI.Models.DTOs;


namespace MarketAPI.Services.Interfaces;

public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync();
        Task<PaymentDto?> GetPaymentByIdAsync(int id);
        Task<IEnumerable<PaymentDto>> GetPaymentsByOrderAsync(int orderId);
        Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto createPaymentDto);
        Task<PaymentDto?> UpdatePaymentAsync(int id, UpdatePaymentDto updatePaymentDto);
        Task<bool> ProcessPaymentAsync(int id);
    }