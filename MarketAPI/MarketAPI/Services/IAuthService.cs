using MarketAPI.Models;
using MarketAPI.Models.DTOs;

namespace MarketAPI.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDTO request);

        Task<TokenResponseDTO?> LoginAsync(LoginDTO request);

        Task<TokenResponseDTO> RefreshTokenAsync(RefreshTokenRequestDTO request);

        Task LogOutAsync(int userId);

    }
}
