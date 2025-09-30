using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Models;

[Table("UserUserRoles")]
public class UserUserRole
{
    [Column("UserId")]
    public int UserId { get; set; }

    [Column("RoleId")]
    public int RoleId { get; set; }

    // Navegación
    public User User { get; set; } = null!;
    public UserRole Role { get; set; } = null!;
}