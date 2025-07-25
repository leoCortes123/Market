using MarketAPI.Custom;
using MarketAPI.Data;
using MarketAPI.Models;
using MarketAPI.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
using System.Runtime.CompilerServices;

namespace MarketAPI.Services
{
    public class AuthService(MarketDbContext context, Utilities utilities ) : IAuthService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly Utilities _utilities = utilities;

        public async Task<TokenResponseDTO?> LoginAsync(LoginDTO request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email && u.IsActive);
            if (user is null)
            {
                logger.Error("Login failed: User not found or inactive.");
                throw new Exception("User not found or inactive.");
            }

            if (!_utilities.VerifyPassword(request.Password, user.PasswordHash))
            {
                logger.Error("Login failed: Invalid password.");
                throw new Exception("Invalid password.");
            }

            return await CreateTokenResponseDTO(user.Id);
        }



        public async Task<User?> RegisterAsync(UserDTO request)
        {
            try
            {
                // Validación del DTO
                if (request == null)
                {
                    logger.Error("La solicitud no puede ser nula");
                    throw new ArgumentNullException(nameof(request), "La solicitud no puede ser nula");
                }

                // Validaciones en paralelo para mejor rendimiento
                bool usernameExists = await context.Users.AnyAsync(u => u.Username == request.Username);
                bool emailExists = await context.Users.AnyAsync(u => u.Email == request.Email);


                if (usernameExists)
                {
                    logger.Error("El nombre de usuario ya está registrado");
                    throw new InvalidOperationException("El nombre de usuario ya está registrado");
                }


                if (emailExists)
                {
                    logger.Error("El correo electrónico ya está registrado");
                    throw new InvalidOperationException("El correo electrónico ya está registrado");
                }


                if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 8)
                {
                    logger.Error("La contraseña debe tener al menos 8 caracteres");
                    throw new ArgumentException("La contraseña debe tener al menos 8 caracteres", nameof(request.Password));
                }


                var passwordHasher = new PasswordHasher<User>();
                var passwordHash = _utilities.EncriptPassword(request.Password);
                    
                var newUser = new User
                {
                    Username = request.Username.Trim(),
                    Email = request.Email.Trim().ToLowerInvariant(),
                    PasswordHash = passwordHash,
                    FullName = request.FullName?.Trim(),
                    Phone = request.Phone?.Trim(),
                    IsFarmerDistributor = request.IsFarmerDistributor,
                    ProfilePicture = request.ProfilePicture,
                    RegisteredAt = DateTime.UtcNow,
                    IsActive = true,
                };

                await context.Users.AddAsync(newUser);
                await context.SaveChangesAsync();

                return newUser;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error al registrar usuario");
                throw new Exception("Error al registrar usuario", ex);
            }
        }

        public async Task<TokenResponseDTO> RefreshTokenAsync(RefreshTokenRequestDTO request)
        {
            var currentRefreshToken = await ValidateRefreshToken(request.UserId, request.RefreshToken);
            return currentRefreshToken == null
                ? throw new Exception("Invalid or expired refresh token.")
                :await CreateTokenResponseDTO(currentRefreshToken.UserId);
        }

        public async Task LogOutAsync(int userId)
        {
            var refreshToken = await context.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == userId);
            if (refreshToken != null)
            {
                context.RefreshTokens.Remove(refreshToken);
                await context.SaveChangesAsync();
            }
        }

        private async Task<TokenResponseDTO> CreateTokenResponseDTO(int userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId && u.IsActive);
            return user is null
                ? throw new Exception("User not found or inactive.")
                : new TokenResponseDTO
                {
                    AccessToken = _utilities.GenerateJwtToken(user.Id, user.Email),
                    RefreshToken = await SaveAndRefreshTokenAsync(userId)
                };
        }

        private async Task<RefreshToken?> ValidateRefreshToken(int userId, string refreshToken)
        {
            var currentRefreshToken = await context.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId ==  userId);
            if (currentRefreshToken == null || currentRefreshToken.Token != refreshToken || currentRefreshToken.ExpiresAt <= DateTime.UtcNow)
            {
                return null;
            }
            return currentRefreshToken;
        }

        private async Task<string> SaveAndRefreshTokenAsync(int userId)
        {
            string refreshToken = _utilities.GenerateRefreshToken();

            var refreshTokenUser = await context.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == userId);
            if (refreshTokenUser == null)
            {
                refreshTokenUser = new RefreshToken()
                {
                    UserId = userId,
                    Token = refreshToken,
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddDays(7)
                };
                context.RefreshTokens.Add(refreshTokenUser);
            }
            else
            {
                refreshTokenUser.Token = refreshToken;
                refreshTokenUser.ExpiresAt = DateTime.UtcNow.AddDays(7);
                context.RefreshTokens.Update(refreshTokenUser);
            }
            await context.SaveChangesAsync();
            return refreshToken;

        }


    }
}