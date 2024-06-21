using Application.Services.Interfaces.IServices.Admin;
using Microsoft.AspNetCore.Mvc;

namespace StoreLineAPI.Controllers.Admin
{
    [ApiController]
    [Route("Admin/User")]
    public class AdminUserController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminUserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/AllInfoAboutUsers")]
        public async Task<IActionResult> GetAllInfoAboutUsersAsync()
        {
            try
            {
                return Ok(await _userService.GetAllInfoAboutUsersAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }
    }
}
