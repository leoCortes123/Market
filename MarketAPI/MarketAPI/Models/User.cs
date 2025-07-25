using System;
using System.Collections.Generic;

namespace MarketAPI.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string? FullName { get; set; }

    public string? Phone { get; set; }

    public bool IsFarmerDistributor { get; set; }

    public string? ProfilePicture { get; set; }

    public DateTime RegisteredAt { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public virtual Supplier? Supplier { get; set; }

    public virtual ICollection<SupplierReview> SupplierReviews { get; set; } = new List<SupplierReview>();

    public virtual ICollection<UserRole> Roles { get; set; } = new List<UserRole>();
}
