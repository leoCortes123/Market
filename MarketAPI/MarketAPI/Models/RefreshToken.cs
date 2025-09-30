using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Models;

[Table("RefreshTokens")]
public class RefreshToken
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required, StringLength(255)]
    public string Token { get; set; } = string.Empty;

    [Required]
    public DateTimeOffset ExpiresAt { get; set; }

    [Required]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? RevokedAt { get; set; }

    // Navegación
    public User User { get; set; } = null!;
}