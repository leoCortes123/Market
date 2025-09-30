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