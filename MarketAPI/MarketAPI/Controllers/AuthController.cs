using MarketAPI.Models;
using MarketAPI.Models.DTOs;
using MarketAPI.Models.DTOs.Auth;
using MarketAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketAPI.Controllers
{


    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserDTO Request)
        {
            try
            {
                var user = await authService.RegisterAsync(Request);

                return Ok(user);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred while registering the user.");
                return StatusCode(500, "An error occurred while registering the user.");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDTO>> Login( LoginDTO request)
        {
            Console.WriteLine("en login api" + request);
            try
            {
                var response = await authService.LoginAsync(request);
                if (response is null)
                {
                    logger.Warn("Login failed: Invalid email or password.");
                    return Unauthorized("Invalid email or password.");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred while logging in the user.");
                return StatusCode(500, "An error occurred while logging in the user.");
            }
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDTO>> RefreshToken([FromBody] RefreshTokenRequestDTO request)
        {
            try
            {
                var response = await authService.RefreshTokenAsync(request);
                if (response is null)
                {
                    logger.Warn("Refresh token failed: Invalid or expired refresh token.");
                    return Unauthorized("Invalid or expired refresh token.");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred while refreshing the token.");
                return StatusCode(500, "An error occurred while refreshing the token.");
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(int userId)
        {
            try
            {
                await authService.LogOutAsync(userId);
                return Ok("Logged out successfully.");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred while logging out the user.");
                return StatusCode(500, "An error occurred while logging out the user.");
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("This endpoint is accessible only to authenticated users.");
        }




    }
}
