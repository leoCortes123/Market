
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Models;

[Table("SupplierReviews")]
public class SupplierReview
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int SupplierId { get; set; }

    public int? UserId { get; set; }

    [Required]
    public int Rating { get; set; }

    public string? Review { get; set; }

    [Required]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    // Navegación
    public Supplier Supplier { get; set; } = null!;
    public User? User { get; set; }
}