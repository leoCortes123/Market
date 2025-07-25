using System;
using System.Collections.Generic;

namespace MarketAPI.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public int UserId { get; set; }

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

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<SupplierCombo> SupplierCombos { get; set; } = new List<SupplierCombo>();

    public virtual ICollection<SupplierProduct> SupplierProducts { get; set; } = new List<SupplierProduct>();

    public virtual ICollection<SupplierReview> SupplierReviews { get; set; } = new List<SupplierReview>();

    public virtual User User { get; set; } = null!;
}
