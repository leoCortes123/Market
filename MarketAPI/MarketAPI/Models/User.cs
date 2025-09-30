using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Models;
[Table("Users")]
public class User
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required, StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(255)]
    public string PasswordHash { get; set; } = string.Empty;

    [StringLength(100)]
    public string? FullName { get; set; }

    [StringLength(30)]
    public string? Phone { get; set; }

    [Required]
    public bool IsFarmerDistributor { get; set; }

    public string? ProfilePicture { get; set; }

    [Required]
    public DateTimeOffset RegisteredAt { get; set; } = DateTimeOffset.UtcNow;

    [Required]
    public bool IsActive { get; set; } = true;

    // Navegación
    public ICollection<UserUserRole> UserUserRoles { get; set; } = new List<UserUserRole>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<SupplierReview> SupplierReviews { get; set; } = new List<SupplierReview>();
}