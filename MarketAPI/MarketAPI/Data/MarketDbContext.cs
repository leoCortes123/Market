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

    public virtual DbSet<Combo> Combos { get; set; }

    public virtual DbSet<ComboProduct> ComboProducts { get; set; }

    public virtual DbSet<MeasurementUnit> MeasurementUnits { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderComboItem> OrderComboItems { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<SupplierCombo> SupplierCombos { get; set; }

    public virtual DbSet<SupplierProduct> SupplierProducts { get; set; }

    public virtual DbSet<SupplierReview> SupplierReviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=MarketApi;User Id=postgres;Password=123456;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Combo>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasPrecision(12, 2);
        });

        modelBuilder.Entity<ComboProduct>(entity =>
        {
            entity.HasKey(e => new { e.ComboId, e.ProductId, e.UnitId });

            entity.HasIndex(e => e.ProductId, "IX_ComboProducts_ProductId");

            entity.HasIndex(e => e.UnitId, "IX_ComboProducts_UnitId");

            entity.Property(e => e.Quantity).HasPrecision(12, 2);

            entity.HasOne(d => d.Combo).WithMany(p => p.ComboProducts).HasForeignKey(d => d.ComboId);

            entity.HasOne(d => d.Product).WithMany(p => p.ComboProducts).HasForeignKey(d => d.ProductId);

            entity.HasOne(d => d.Unit).WithMany(p => p.ComboProducts).HasForeignKey(d => d.UnitId);
        });

        modelBuilder.Entity<MeasurementUnit>(entity =>
        {
            entity.Property(e => e.Abbreviation).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.SupplierId, "IX_Orders_SupplierId");

            entity.HasIndex(e => e.UserId, "IX_Orders_UserId");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus).HasMaxLength(20);
            entity.Property(e => e.Status).HasMaxLength(30);
            entity.Property(e => e.TotalAmount).HasPrecision(12, 2);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Orders).HasForeignKey(d => d.SupplierId);

            entity.HasOne(d => d.User).WithMany(p => p.Orders).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<OrderComboItem>(entity =>
        {
            entity.HasIndex(e => e.OrderId, "IX_OrderComboItems_OrderId");

            entity.HasIndex(e => e.SupplierComboId, "IX_OrderComboItems_SupplierComboId");

            entity.Property(e => e.Price).HasPrecision(12, 2);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderComboItems).HasForeignKey(d => d.OrderId);

            entity.HasOne(d => d.SupplierCombo).WithMany(p => p.OrderComboItems).HasForeignKey(d => d.SupplierComboId);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasIndex(e => e.MeasurementUnitId, "IX_OrderItems_MeasurementUnitId");

            entity.HasIndex(e => e.OrderId, "IX_OrderItems_OrderId");

            entity.HasIndex(e => e.SupplierProductId, "IX_OrderItems_SupplierProductId");

            entity.HasIndex(e => e.UnitId, "IX_OrderItems_UnitId");

            entity.Property(e => e.Price).HasPrecision(12, 2);
            entity.Property(e => e.Quantity).HasPrecision(12, 2);

            entity.HasOne(d => d.MeasurementUnit).WithMany(p => p.OrderItemMeasurementUnits).HasForeignKey(d => d.MeasurementUnitId);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems).HasForeignKey(d => d.OrderId);

            entity.HasOne(d => d.SupplierProduct).WithMany(p => p.OrderItems).HasForeignKey(d => d.SupplierProductId);

            entity.HasOne(d => d.Unit).WithMany(p => p.OrderItemUnits)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasIndex(e => e.OrderId, "IX_Payments_OrderId");

            entity.Property(e => e.Amount).HasPrecision(12, 2);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.Currency).HasMaxLength(10);
            entity.Property(e => e.PaymentProvider).HasMaxLength(50);
            entity.Property(e => e.ProviderPaymentId).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(30);

            entity.HasOne(d => d.Order).WithMany(p => p.Payments).HasForeignKey(d => d.OrderId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.DefaultUnitId, "IX_Products_DefaultUnitId");

            entity.HasIndex(e => e.IsActive, "IX_Products_IsActive");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.DefaultUnit).WithMany(p => p.Products).HasForeignKey(d => d.DefaultUnitId);
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_RefreshTokens_UserId");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.Token).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Suppliers_UserId").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.DisplayName).HasMaxLength(100);
            entity.Property(e => e.State).HasMaxLength(50);
            entity.Property(e => e.ZipCode).HasMaxLength(20);

            entity.HasOne(d => d.User).WithOne(p => p.Supplier).HasForeignKey<Supplier>(d => d.UserId);
        });

        modelBuilder.Entity<SupplierCombo>(entity =>
        {
            entity.HasIndex(e => e.ComboId, "IX_SupplierCombos_ComboId");

            entity.HasIndex(e => e.SupplierId, "IX_SupplierCombos_SupplierId");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Price).HasPrecision(12, 2);

            entity.HasOne(d => d.Combo).WithMany(p => p.SupplierCombos).HasForeignKey(d => d.ComboId);

            entity.HasOne(d => d.Supplier).WithMany(p => p.SupplierCombos).HasForeignKey(d => d.SupplierId);
        });

        modelBuilder.Entity<SupplierProduct>(entity =>
        {
            entity.HasIndex(e => e.MeasurementUnitId, "IX_SupplierProducts_MeasurementUnitId");

            entity.HasIndex(e => e.ProductId, "IX_SupplierProducts_ProductId");

            entity.HasIndex(e => e.SupplierId, "IX_SupplierProducts_SupplierId");

            entity.HasIndex(e => e.UnitId, "IX_SupplierProducts_UnitId");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Price).HasPrecision(12, 2);
            entity.Property(e => e.Stock).HasPrecision(12, 2);

            entity.HasOne(d => d.MeasurementUnit).WithMany(p => p.SupplierProductMeasurementUnits).HasForeignKey(d => d.MeasurementUnitId);

            entity.HasOne(d => d.Product).WithMany(p => p.SupplierProducts).HasForeignKey(d => d.ProductId);

            entity.HasOne(d => d.Supplier).WithMany(p => p.SupplierProducts).HasForeignKey(d => d.SupplierId);

            entity.HasOne(d => d.Unit).WithMany(p => p.SupplierProductUnits)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<SupplierReview>(entity =>
        {
            entity.HasIndex(e => e.SupplierId, "IX_SupplierReviews_SupplierId");

            entity.HasIndex(e => e.UserId, "IX_SupplierReviews_UserId");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Supplier).WithMany(p => p.SupplierReviews).HasForeignKey(d => d.SupplierId);

            entity.HasOne(d => d.User).WithMany(p => p.SupplierReviews).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.RegisteredAt).HasDefaultValueSql("now()");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserUserRole",
                    r => r.HasOne<UserRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("UserUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_UserUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
