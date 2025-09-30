namespace MarketAPI.Models.DTOs.Auth
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string? FullName { get; set; }

        public string? Phone { get; set; }

        public bool IsFarmerDistributor { get; set; }

        public string? ProfilePicture { get; set; }

        public DateTime RegisteredAt { get; set; }

        public bool IsActive { get; set; }

    }
}
