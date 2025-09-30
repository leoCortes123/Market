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


public class OrderService : IOrderService
    {
        private readonly MarketDbContext _context;

        public OrderService(MarketDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Supplier)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.SupplierProduct)
                        .ThenInclude(sp => sp.Product)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Unit)
                .Include(o => o.OrderComboItems)
                    .ThenInclude(oci => oci.SupplierCombo)
                        .ThenInclude(sc => sc.Combo)
                .Include(o => o.Payments)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    SupplierId = o.SupplierId,
                    Status = o.Status,
                    TotalAmount = o.TotalAmount,
                    PaymentMethod = o.PaymentMethod,
                    PaymentStatus = o.PaymentStatus,
                    ShippingAddress = o.ShippingAddress,
                    CreatedAt = o.CreatedAt,
                    UpdatedAt = o.UpdatedAt,
                    UserName = o.User!.Username,
                    SupplierName = o.Supplier!.DisplayName,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDto
                    {
                        Id = oi.Id,
                        OrderId = oi.OrderId,
                        SupplierProductId = oi.SupplierProductId,
                        Quantity = oi.Quantity,
                        UnitId = oi.UnitId,
                        Price = oi.Price,
                        ProductName = oi.SupplierProduct!.Product.Name,
                        UnitName = oi.Unit!.Name
                    }).ToList(),
                    OrderComboItems = o.OrderComboItems.Select(oci => new OrderComboItemDto
                    {
                        Id = oci.Id,
                        OrderId = oci.OrderId,
                        SupplierComboId = oci.SupplierComboId,
                        Quantity = oci.Quantity,
                        Price = oci.Price,
                        ComboName = oci.SupplierCombo!.Combo.Name
                    }).ToList(),
                    Payments = o.Payments.Select(p => new PaymentDto
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
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Supplier)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.SupplierProduct)
                        .ThenInclude(sp => sp.Product)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Unit)
                .Include(o => o.OrderComboItems)
                    .ThenInclude(oci => oci.SupplierCombo)
                        .ThenInclude(sc => sc.Combo)
                .Include(o => o.Payments)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return null;

            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                SupplierId = order.SupplierId,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                PaymentMethod = order.PaymentMethod,
                PaymentStatus = order.PaymentStatus,
                ShippingAddress = order.ShippingAddress,
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt,
                UserName = order.User!.Username,
                SupplierName = order.Supplier!.DisplayName,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    OrderId = oi.OrderId,
                    SupplierProductId = oi.SupplierProductId,
                    Quantity = oi.Quantity,
                    UnitId = oi.UnitId,
                    Price = oi.Price,
                    ProductName = oi.SupplierProduct!.Product.Name,
                    UnitName = oi.Unit!.Name
                }).ToList(),
                OrderComboItems = order.OrderComboItems.Select(oci => new OrderComboItemDto
                {
                    Id = oci.Id,
                    OrderId = oci.OrderId,
                    SupplierComboId = oci.SupplierComboId,
                    Quantity = oci.Quantity,
                    Price = oci.Price,
                    ComboName = oci.SupplierCombo!.Combo.Name
                }).ToList(),
                Payments = order.Payments.Select(p => new PaymentDto
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
                }).ToList()
            };
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserAsync(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.User)
                .Include(o => o.Supplier)
                .Include(o => o.OrderItems)
                .Include(o => o.OrderComboItems)
                .Include(o => o.Payments)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    SupplierId = o.SupplierId,
                    Status = o.Status,
                    TotalAmount = o.TotalAmount,
                    PaymentMethod = o.PaymentMethod,
                    PaymentStatus = o.PaymentStatus,
                    ShippingAddress = o.ShippingAddress,
                    CreatedAt = o.CreatedAt,
                    UpdatedAt = o.UpdatedAt,
                    UserName = o.User.Username,
                    SupplierName = o.Supplier!.DisplayName
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersBySupplierAsync(int supplierId)
        {
            return await _context.Orders
                .Where(o => o.SupplierId == supplierId)
                .Include(o => o.User)
                .Include(o => o.Supplier)
                .Include(o => o.OrderItems)
                .Include(o => o.OrderComboItems)
                .Include(o => o.Payments)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    SupplierId = o.SupplierId,
                    Status = o.Status,
                    TotalAmount = o.TotalAmount,
                    PaymentMethod = o.PaymentMethod,
                    PaymentStatus = o.PaymentStatus,
                    ShippingAddress = o.ShippingAddress,
                    CreatedAt = o.CreatedAt,
                    UpdatedAt = o.UpdatedAt,
                    UserName = o.User!.Username,
                    SupplierName = o.Supplier!.DisplayName
                })
                .ToListAsync();
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Calcular el monto total
                decimal totalAmount = 0;

                // Calcular total de items
                foreach (var item in createOrderDto.OrderItems)
                {
                    totalAmount += item.Quantity * item.Price;
                }

                // Calcular total de combos
                foreach (var comboItem in createOrderDto.OrderComboItems)
                {
                    totalAmount += comboItem.Quantity * comboItem.Price;
                }

                var order = new Order
                {
                    UserId = createOrderDto.UserId,
                    SupplierId = createOrderDto.SupplierId,
                    Status = "pending",
                    TotalAmount = totalAmount,
                    PaymentMethod = createOrderDto.PaymentMethod,
                    PaymentStatus = "pending",
                    ShippingAddress = createOrderDto.ShippingAddress,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // Agregar items
                foreach (var item in createOrderDto.OrderItems)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        SupplierProductId = item.SupplierProductId,
                        Quantity = item.Quantity,
                        UnitId = item.UnitId,
                        Price = item.Price
                    };
                    _context.OrderItems.Add(orderItem);
                }

                // Agregar combos
                foreach (var comboItem in createOrderDto.OrderComboItems)
                {
                    var orderComboItem = new OrderComboItem
                    {
                        OrderId = order.Id,
                        SupplierComboId = comboItem.SupplierComboId,
                        Quantity = comboItem.Quantity,
                        Price = comboItem.Price
                    };
                    _context.OrderComboItems.Add(orderComboItem);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await GetOrderByIdAsync(order.Id) ?? throw new Exception("Error al crear la orden");
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<OrderDto?> UpdateOrderAsync(int id, UpdateOrderDto updateOrderDto)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return null;

            order.Status = updateOrderDto.Status;
            order.PaymentStatus = updateOrderDto.PaymentStatus;
            order.ShippingAddress = updateOrderDto.ShippingAddress;
            order.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetOrderByIdAsync(id);
        }

        public async Task<bool> CancelOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;

            if (order.Status == "completed" || order.Status == "shipped")
            {
                throw new InvalidOperationException("No se puede cancelar una orden completada o enviada");
            }

            order.Status = "cancelled";
            order.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.OrderComboItems)
                .Include(o => o.Payments)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return false;

            _context.OrderItems.RemoveRange(order.OrderItems);
            _context.OrderComboItems.RemoveRange(order.OrderComboItems);
            _context.Payments.RemoveRange(order.Payments);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }