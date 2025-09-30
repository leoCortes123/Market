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


public class SupplierService : ISupplierService
    {
        private readonly MarketDbContext _context;

        public SupplierService(MarketDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync()
        {
            return await _context.Suppliers
                .Include(s => s.User)
                .Include(s => s.SupplierProducts)
                .Include(s => s.SupplierCombos)
                .Select(s => new SupplierDto
                {
                    Id = s.Id,
                    UserId = s.UserId,
                    DisplayName = s.DisplayName,
                    About = s.About,
                    BlogContent = s.BlogContent,
                    Address = s.Address,
                    Country = s.Country,
                    State = s.State,
                    City = s.City,
                    ZipCode = s.ZipCode,
                    ProfileBannerUrl = s.ProfileBannerUrl,
                    FacebookUrl = s.FacebookUrl,
                    InstagramUrl = s.InstagramUrl,
                    TwitterUrl = s.TwitterUrl,
                    WebsiteUrl = s.WebsiteUrl,
                    UserName = s.User.Username,
                    Email = s.User.Email,
                    ProductCount = s.SupplierProducts.Count(sp => sp.IsActive),
                    ComboCount = s.SupplierCombos.Count(sc => sc.IsActive)
                })
                .ToListAsync();
        }

        public async Task<SupplierDto?> GetSupplierByIdAsync(int id)
        {
            var supplier = await _context.Suppliers
                .Include(s => s.User)
                .Include(s => s.SupplierProducts)
                .Include(s => s.SupplierCombos)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (supplier == null) return null;

            return new SupplierDto
            {
                Id = supplier.Id,
                UserId = supplier.UserId,
                DisplayName = supplier.DisplayName,
                About = supplier.About,
                BlogContent = supplier.BlogContent,
                Address = supplier.Address,
                Country = supplier.Country,
                State = supplier.State,
                City = supplier.City,
                ZipCode = supplier.ZipCode,
                ProfileBannerUrl = supplier.ProfileBannerUrl,
                FacebookUrl = supplier.FacebookUrl,
                InstagramUrl = supplier.InstagramUrl,
                TwitterUrl = supplier.TwitterUrl,
                WebsiteUrl = supplier.WebsiteUrl,
                UserName = supplier.User.Username,
                Email = supplier.User.Email,
                ProductCount = supplier.SupplierProducts.Count(sp => sp.IsActive),
                ComboCount = supplier.SupplierCombos.Count(sc => sc.IsActive)
            };
        }

        public async Task<SupplierDto?> GetSupplierByUserIdAsync(int userId)
        {
            var supplier = await _context.Suppliers
                .Include(s => s.User)
                .Include(s => s.SupplierProducts)
                .Include(s => s.SupplierCombos)
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (supplier == null) return null;

            return new SupplierDto
            {
                Id = supplier.Id,
                UserId = supplier.UserId,
                DisplayName = supplier.DisplayName,
                About = supplier.About,
                BlogContent = supplier.BlogContent,
                Address = supplier.Address,
                Country = supplier.Country,
                State = supplier.State,
                City = supplier.City,
                ZipCode = supplier.ZipCode,
                ProfileBannerUrl = supplier.ProfileBannerUrl,
                FacebookUrl = supplier.FacebookUrl,
                InstagramUrl = supplier.InstagramUrl,
                TwitterUrl = supplier.TwitterUrl,
                WebsiteUrl = supplier.WebsiteUrl,
                UserName = supplier.User.Username,
                Email = supplier.User.Email,
                ProductCount = supplier.SupplierProducts.Count(sp => sp.IsActive),
                ComboCount = supplier.SupplierCombos.Count(sc => sc.IsActive)
            };
        }

        public async Task<SupplierDto> CreateSupplierAsync(CreateSupplierDto createSupplierDto)
        {
            // Verificar si el usuario ya es proveedor
            var existingSupplier = await _context.Suppliers
                .FirstOrDefaultAsync(s => s.UserId == createSupplierDto.UserId);

            if (existingSupplier != null)
            {
                throw new InvalidOperationException("El usuario ya es un proveedor");
            }

            var supplier = new Supplier
            {
                UserId = createSupplierDto.UserId,
                DisplayName = createSupplierDto.DisplayName,
                About = createSupplierDto.About,
                BlogContent = createSupplierDto.BlogContent,
                Address = createSupplierDto.Address,
                Country = createSupplierDto.Country,
                State = createSupplierDto.State,
                City = createSupplierDto.City,
                ZipCode = createSupplierDto.ZipCode,
                ProfileBannerUrl = createSupplierDto.ProfileBannerUrl,
                FacebookUrl = createSupplierDto.FacebookUrl,
                InstagramUrl = createSupplierDto.InstagramUrl,
                TwitterUrl = createSupplierDto.TwitterUrl,
                WebsiteUrl = createSupplierDto.WebsiteUrl
            };

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return await GetSupplierByIdAsync(supplier.Id) ?? throw new Exception("Error al crear el proveedor");
        }

        public async Task<SupplierDto?> UpdateSupplierAsync(int id, UpdateSupplierDto updateSupplierDto)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null) return null;

            supplier.DisplayName = updateSupplierDto.DisplayName;
            supplier.About = updateSupplierDto.About;
            supplier.BlogContent = updateSupplierDto.BlogContent;
            supplier.Address = updateSupplierDto.Address;
            supplier.Country = updateSupplierDto.Country;
            supplier.State = updateSupplierDto.State;
            supplier.City = updateSupplierDto.City;
            supplier.ZipCode = updateSupplierDto.ZipCode;
            supplier.ProfileBannerUrl = updateSupplierDto.ProfileBannerUrl;
            supplier.FacebookUrl = updateSupplierDto.FacebookUrl;
            supplier.InstagramUrl = updateSupplierDto.InstagramUrl;
            supplier.TwitterUrl = updateSupplierDto.TwitterUrl;
            supplier.WebsiteUrl = updateSupplierDto.WebsiteUrl;

            await _context.SaveChangesAsync();

            return await GetSupplierByIdAsync(id);
        }

        public async Task<bool> DeleteSupplierAsync(int id)
        {
            var supplier = await _context.Suppliers
                .Include(s => s.SupplierProducts)
                .Include(s => s.SupplierCombos)
                .Include(s => s.Orders)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (supplier == null) return false;

            // Verificar si hay órdenes asociadas
            if (supplier.Orders.Any())
            {
                throw new InvalidOperationException("No se puede eliminar el proveedor porque tiene órdenes asociadas");
            }

            _context.SupplierProducts.RemoveRange(supplier.SupplierProducts);
            _context.SupplierCombos.RemoveRange(supplier.SupplierCombos);
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProductDto>> GetSupplierProductsAsync(int supplierId)
        {
            return await _context.SupplierProducts
                .Where(sp => sp.SupplierId == supplierId && sp.IsActive)
                .Include(sp => sp.Product)
                    .ThenInclude(p => p.Category)
                .Include(sp => sp.Product)
                    .ThenInclude(p => p.MeasurementUnit)
                .Select(sp => new ProductDto
                {
                    Id = sp.Product.Id,
                    Name = sp.Product.Name,
                    Description = sp.Product.Description,
                    MeasurementUnitId = sp.Product.MeasurementUnitId,
                    ImageUrl = sp.Product.ImageUrl,
                    IsActive = sp.Product.IsActive,
                    CategoryId = sp.Product.CategoryId,
                    CategoryName = sp.Product.Category.Name,
                    MeasurementUnitName = sp.Product.MeasurementUnit!.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ComboDto>> GetSupplierCombosAsync(int supplierId)
        {
            return await _context.SupplierCombos
                .Where(sc => sc.SupplierId == supplierId && sc.IsActive)
                .Include(sc => sc.Combo)
                    .ThenInclude(c => c.ComboProducts)
                        .ThenInclude(cp => cp.Product)
                .Include(sc => sc.Combo)
                    .ThenInclude(c => c.ComboProducts)
                        .ThenInclude(cp => cp.Unit)
                .Select(sc => new ComboDto
                {
                    Id = sc.Combo.Id,
                    Name = sc.Combo.Name,
                    Description = sc.Combo.Description,
                    Price = sc.Price, // Precio del proveedor
                    ImageUrl = sc.Combo.ImageUrl,
                    IsActive = sc.Combo.IsActive,
                    ComboProducts = sc.Combo.ComboProducts.Select(cp => new ComboProductDetailDto
                    {
                        ProductId = cp.ProductId,
                        ProductName = cp.Product.Name,
                        UnitId = cp.UnitId,
                        UnitName = cp.Unit.Name,
                        Quantity = cp.Quantity
                    }).ToList()
                })
                .ToListAsync();
        }
    }