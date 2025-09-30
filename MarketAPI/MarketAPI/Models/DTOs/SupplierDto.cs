namespace MarketAPI.Models.DTOs;

public class SupplierDto
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
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public int ProductCount { get; set; }
        public int ComboCount { get; set; }
    }

    public class CreateSupplierDto
    {
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
    }

    public class UpdateSupplierDto
    {
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
    }