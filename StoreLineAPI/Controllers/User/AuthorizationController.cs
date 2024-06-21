using Application.DtoModels.Models.Admin;
using Application.DtoModels.Models.Authorization;
using Application.Services.Interfaces.IServices.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StoreLineAPI.Controllers.User
{
    [ApiController]
    [Route("/Authorization")]
    public class AuthorizationController : Controller
    {
        private readonly IAccountService _accountService;

        public AuthorizationController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto model)
        {
            try
            {
                return Ok(await _accountService.LoginAsync(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto model)
        {
            try
            {
                return Ok(await _accountService.RegisterAsync(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("Logout")]
        [AllowAnonymous]
        public async Task<IActionResult> LogoutAsync()
        {
            try
            {
                return Ok(await _accountService.LogoutAsync(HttpContext));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
        {
            await _accountService.ForgotPasswordAsync(model);
            return Ok(new { Message = "Password reset link has been sent to your email." });
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            await _accountService.ResetPasswordAsync(model);
            return Ok(new { Message = "Password has been reset successfully." });
        }
    }
}
