using Application.Services.Interfaces.IServices.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StoreLineAPI.Controllers.User
{
    [ApiController]
    [Route("/Store")]
    //[Authorize]
    public class InformationAboutStoresController : ControllerBase
    {
        private readonly IInformationAboutStoreService _storeService;

        public InformationAboutStoresController(IInformationAboutStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet("AllStores")]
        public async Task<IActionResult> GetAllStoresAsync()
        {
            try
            {
                return Ok(await _storeService.GetAllStoresAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

        [HttpGet("Chains")]
        public async Task<IActionResult> GetAllChainsAsync()
        {
            try
            {
                return Ok(await _storeService.GetAllChainsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}