using System;
using System.Collections.Generic;
using MarketAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketAPI.Data;

public partial class MarketDbContext : DbContext
{
    public MarketDbContext()
    {
    }

    public MarketDbContext(DbContextOptions<MarketDbContext> options)
        : base(options)
    {
    }

    // DbSets
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<MeasurementUnit> MeasurementUnits { get; set; } = null!;
    public DbSet<Combo> Combos { get; set; } = null!;
    public DbSet<ComboProduct> ComboProducts { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserRole> UserRoles { get; set; } = null!;
    public DbSet<UserUserRole> UserUserRoles { get; set; } = null!;
    public DbSet<Supplier> Suppliers { get; set; } = null!;
    public DbSet<SupplierProduct> SupplierProducts { get; set; } = null!;
    public DbSet<SupplierCombo> SupplierCombos { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    public DbSet<OrderComboItem> OrderComboItems { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
    public DbSet<SupplierReview> SupplierReviews { get; set; } = null!;
    public DbSet<Calendar> Calendars { get; set; } = null!;
    public DbSet<Event> Events { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración de claves primarias con generación automática
        ConfigurePrimaryKeys(modelBuilder);

        // Claves compuestas
        modelBuilder.Entity<ComboProduct>()
            .HasKey(cp => new { cp.ComboId, cp.ProductId, cp.UnitId });

        modelBuilder.Entity<UserUserRole>()
            .HasKey(uur => new { uur.UserId, uur.RoleId });

        // Relaciones
        ConfigureRelationships(modelBuilder);

        // Configuración de valores por defecto
        ConfigureDefaultValues(modelBuilder);

        // Timestamps
        ConfigureTimestamps(modelBuilder);

        // ---- SEED DATA ----

        // Roles
        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { Id = 1, Name = "Admin" },
            new UserRole { Id = 2, Name = "Customer" },
            new UserRole { Id = 3, Name = "Supplier" }
        );

        // Unidades de medida
        modelBuilder.Entity<MeasurementUnit>().HasData(
            new MeasurementUnit { Id = 1, Name = "Gramo", Abbreviation = "g", IsWeight = true, WeightInGrams = 1 },
            new MeasurementUnit { Id = 2, Name = "Kilogramo", Abbreviation = "kg", IsWeight = true, WeightInGrams = 1000 },
            new MeasurementUnit { Id = 3, Name = "Libra", Abbreviation = "lb", IsWeight = true, WeightInGrams = 453 },
            new MeasurementUnit { Id = 4, Name = "Mililitro", Abbreviation = "ml", IsWeight = false },
            new MeasurementUnit { Id = 5, Name = "Litro", Abbreviation = "l", IsWeight = false },
            new MeasurementUnit { Id = 6, Name = "Unidad", Abbreviation = "un", IsWeight = false },
            new MeasurementUnit { Id = 7, Name = "Docena", Abbreviation = "doc", IsWeight = false },
            new MeasurementUnit { Id = 8, Name = "Paquete", Abbreviation = "pq", IsWeight = false },
            new MeasurementUnit { Id = 9, Name = "Caja", Abbreviation = "cj", IsWeight = false }
        );

        // Categorías
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Frutas y Verduras", Description = "Productos frescos del campo" },
            new Category { Id = 2, Name = "Lácteos y Huevos", Description = "Productos lácteos y huevos frescos" },
            new Category { Id = 3, Name = "Granos y Cereales", Description = "Arroz, frijol, maíz y más" },
            new Category { Id = 4, Name = "Carnes y Embutidos", Description = "Carnes frescas y procesadas" },
            new Category { Id = 5, Name = "Panadería y Desayuno", Description = "Arepas, pan, café y más" },
            new Category { Id = 6, Name = "Otros", Description = "Productos varios" }
        );

        // Productos
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Manzana", Description = "Manzanas rojas frescas", MeasurementUnitId = 6, ImageUrl = "manzana.jpg", IsActive = true, CategoryId = 1 },
            new Product { Id = 2, Name = "Banano", Description = "Bananos maduros de exportación", MeasurementUnitId = 6, ImageUrl = "banano.jpg", IsActive = true, CategoryId = 1 },
            new Product { Id = 3, Name = "Queso Campesino", Description = "Queso fresco campesino 250g", MeasurementUnitId = 1, ImageUrl = "queso.jpg", IsActive = true, CategoryId = 2 },
            new Product { Id = 4, Name = "Arepa de Maíz", Description = "Arepas de maíz blanco x6 unidades", MeasurementUnitId = 8, ImageUrl = "arepa.jpg", IsActive = true, CategoryId = 5 },
            new Product { Id = 5, Name = "Café Molido", Description = "Café molido 100% colombiano 500g", MeasurementUnitId = 1, ImageUrl = "cafe.jpg", IsActive = true, CategoryId = 5 }
        );

        // Combos
        modelBuilder.Entity<Combo>().HasData(
            new Combo { Id = 1, Name = "Desayuno Campesino", Description = "Incluye arepas, queso y café", Price = 25000, ImageUrl = "desayuno.jpg", IsActive = true }
        );

        // Relación Combo-Producto
        modelBuilder.Entity<ComboProduct>().HasData(
            new { ComboId = 1, ProductId = 3, UnitId = 1, Quantity = 0.25m }, // Queso
            new { ComboId = 1, ProductId = 4, UnitId = 8, Quantity = 1m },    // Arepas
            new { ComboId = 1, ProductId = 5, UnitId = 1, Quantity = 0.5m }   // Café
        );


        base.OnModelCreating(modelBuilder);
    }

    private void ConfigurePrimaryKeys(ModelBuilder modelBuilder)
    {
        // Configurar todas las entidades con propiedad "Id" como clave primaria auto-generada
        modelBuilder.Entity<Category>()
            .HasKey(e => e.Id)
            .HasName("PK_Categories");
        modelBuilder.Entity<Category>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Product>()
            .HasKey(e => e.Id)
            .HasName("PK_Products");
        modelBuilder.Entity<Product>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<MeasurementUnit>()
            .HasKey(e => e.Id)
            .HasName("PK_MeasurementUnits");
        modelBuilder.Entity<MeasurementUnit>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Combo>()
            .HasKey(e => e.Id)
            .HasName("PK_Combos");
        modelBuilder.Entity<Combo>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<User>()
            .HasKey(e => e.Id)
            .HasName("PK_Users");
        modelBuilder.Entity<User>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<UserRole>()
            .HasKey(e => e.Id)
            .HasName("PK_UserRoles");
        modelBuilder.Entity<UserRole>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Supplier>()
            .HasKey(e => e.Id)
            .HasName("PK_Suppliers");
        modelBuilder.Entity<Supplier>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Order>()
            .HasKey(e => e.Id)
            .HasName("PK_Orders");
        modelBuilder.Entity<Order>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<OrderItem>()
            .HasKey(e => e.Id)
            .HasName("PK_OrderItems");
        modelBuilder.Entity<OrderItem>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<OrderComboItem>()
            .HasKey(e => e.Id)
            .HasName("PK_OrderComboItems");
        modelBuilder.Entity<OrderComboItem>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Payment>()
            .HasKey(e => e.Id)
            .HasName("PK_Payments");
        modelBuilder.Entity<Payment>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<RefreshToken>()
            .HasKey(e => e.Id)
            .HasName("PK_RefreshTokens");
        modelBuilder.Entity<RefreshToken>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<SupplierReview>()
            .HasKey(e => e.Id)
            .HasName("PK_SupplierReviews");
        modelBuilder.Entity<SupplierReview>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<SupplierProduct>()
            .HasKey(e => e.Id)
            .HasName("PK_SupplierProducts");
        modelBuilder.Entity<SupplierProduct>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<SupplierCombo>()
            .HasKey(e => e.Id)
            .HasName("PK_SupplierCombos");
        modelBuilder.Entity<SupplierCombo>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Calendar>()
            .HasKey(e => e.Id)
            .HasName("PK_Calendars");
        modelBuilder.Entity<Calendar>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Event>()
            .HasKey(e => e.Id)
            .HasName("PK_Events");
        modelBuilder.Entity<Event>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();
    }

    private void ConfigureRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComboProduct>()
            .HasOne(cp => cp.Combo)
            .WithMany(c => c.ComboProducts)
            .HasForeignKey(cp => cp.ComboId);

        modelBuilder.Entity<ComboProduct>()
            .HasOne(cp => cp.Product)
            .WithMany()
            .HasForeignKey(cp => cp.ProductId);

        modelBuilder.Entity<ComboProduct>()
            .HasOne(cp => cp.Unit)
            .WithMany()
            .HasForeignKey(cp => cp.UnitId);

        modelBuilder.Entity<UserUserRole>()
            .HasOne(uur => uur.User)
            .WithMany(u => u.UserUserRoles)
            .HasForeignKey(uur => uur.UserId);

        modelBuilder.Entity<UserUserRole>()
            .HasOne(uur => uur.Role)
            .WithMany(r => r.UserUserRoles)
            .HasForeignKey(uur => uur.RoleId);

        modelBuilder.Entity<Supplier>()
            .HasOne(s => s.User)
            .WithMany()
            .HasForeignKey(s => s.UserId);

        modelBuilder.Entity<SupplierProduct>()
            .HasOne(sp => sp.Supplier)
            .WithMany(s => s.SupplierProducts)
            .HasForeignKey(sp => sp.SupplierId);

        modelBuilder.Entity<SupplierProduct>()
            .HasOne(sp => sp.Product)
            .WithMany()
            .HasForeignKey(sp => sp.ProductId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Supplier)
            .WithMany(s => s.Orders)
            .HasForeignKey(o => o.SupplierId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<OrderComboItem>()
            .HasOne(oci => oci.Order)
            .WithMany(o => o.OrderComboItems)
            .HasForeignKey(oci => oci.OrderId);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Order)
            .WithMany(o => o.Payments)
            .HasForeignKey(p => p.OrderId);

        modelBuilder.Entity<RefreshToken>()
            .HasOne(rt => rt.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserId);

        modelBuilder.Entity<SupplierReview>()
            .HasOne(sr => sr.Supplier)
            .WithMany(s => s.SupplierReviews)
            .HasForeignKey(sr => sr.SupplierId);

        modelBuilder.Entity<SupplierReview>()
            .HasOne(sr => sr.User)
            .WithMany(u => u.SupplierReviews)
            .HasForeignKey(sr => sr.UserId);
    }

    private void ConfigureDefaultValues(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Combo>()
            .Property(c => c.IsActive)
            .HasDefaultValue(true);

        modelBuilder.Entity<Product>()
            .Property(p => p.IsActive)
            .HasDefaultValue(true);

        modelBuilder.Entity<SupplierProduct>()
            .Property(sp => sp.IsActive)
            .HasDefaultValue(true);

        modelBuilder.Entity<SupplierCombo>()
            .Property(sc => sc.IsActive)
            .HasDefaultValue(true);

        modelBuilder.Entity<User>()
            .Property(u => u.IsActive)
            .HasDefaultValue(true);
        modelBuilder.Entity<Event>()
            .HasOne(e => e.Calendar)
            .WithMany(c => c.Events)
            .HasForeignKey(e => e.CalendarId);
    }

    private void ConfigureTimestamps(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.ClrType == typeof(Order))
            {
                modelBuilder.Entity<Order>()
                    .Property(o => o.CreatedAt)
                    .HasDefaultValueSql("now()");

                modelBuilder.Entity<Order>()
                    .Property(o => o.UpdatedAt)
                    .HasDefaultValueSql("now()");
            }
            else if (entityType.ClrType == typeof(Payment))
            {
                modelBuilder.Entity<Payment>()
                    .Property(p => p.CreatedAt)
                    .HasDefaultValueSql("now()");
            }
            else if (entityType.ClrType == typeof(RefreshToken))
            {
                modelBuilder.Entity<RefreshToken>()
                    .Property(rt => rt.CreatedAt)
                    .HasDefaultValueSql("now()");
            }
            else if (entityType.ClrType == typeof(SupplierProduct) ||
                     entityType.ClrType == typeof(SupplierCombo) ||
                     entityType.ClrType == typeof(SupplierReview))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property("CreatedAt")
                    .HasDefaultValueSql("now()");
            }
        }
    }
}