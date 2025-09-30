using MarketAPI.Models.DTOs;


namespace MarketAPI.Services.Interfaces;

public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto?> GetOrderByIdAsync(int id);
        Task<IEnumerable<OrderDto>> GetOrdersByUserAsync(int userId);
        Task<IEnumerable<OrderDto>> GetOrdersBySupplierAsync(int supplierId);
        Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto);
        Task<OrderDto?> UpdateOrderAsync(int id, UpdateOrderDto updateOrderDto);
        Task<bool> CancelOrderAsync(int id);
        Task<bool> DeleteOrderAsync(int id);
    }