
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Models;

[Table("Suppliers")]
public class Supplier
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [StringLength(100)]
    public string? DisplayName { get; set; }

    public string? About { get; set; }
    public string? BlogContent { get; set; }
    public string? Address { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }
    public string? ProfileBannerUrl { get; set; }
    public string? FacebookUrl { get; set; }
    public string? InstagramUrl { get; set; }
    public string? TwitterUrl { get; set; }
    public string? WebsiteUrl { get; set; }

    // Navegación
    public User User { get; set; } = null!;
    public ICollection<SupplierProduct> SupplierProducts { get; set; } = new List<SupplierProduct>();
    public ICollection<SupplierCombo> SupplierCombos { get; set; } = new List<SupplierCombo>();
    public ICollection<SupplierReview> SupplierReviews { get; set; } = new List<SupplierReview>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}