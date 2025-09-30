using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Models;

[Table("UserRoles")]
public class UserRole
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(30)]
    public string Name { get; set; } = string.Empty;

    // Navegación
    public ICollection<UserUserRole> UserUserRoles { get; set; } = new List<UserUserRole>();
}