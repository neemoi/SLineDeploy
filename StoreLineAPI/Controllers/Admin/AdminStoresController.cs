using Application.DtoModels.Models.Admin;
using Application.Services.Interfaces.IServices.Admin;
using Microsoft.AspNetCore.Mvc;

namespace StoreLineAPI.Controllers.Admin
{
    [ApiController]
    [Route("Admin/Store")]
    public class AdminStoresController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly IChainOfStoreService _chainOfStoresService;

        public AdminStoresController(IStoreService storeService, IChainOfStoreService chainOfStoresService)
        {
            _storeService = storeService;
            _chainOfStoresService = chainOfStoresService;
        }

        [HttpGet("/GetAllStore")]
        public async Task<IActionResult> GetAllStoreAsync()
        {
            try
            {
                return Ok(await _storeService.GetAllStoreAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

        [HttpGet("/GetAllChainOfStore")]
        public async Task<IActionResult> GetAllChainOfStoreAsync()
        {
            try
            {
                return Ok(await _chainOfStoresService.GetAllChainOfStoreAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

        [HttpPost("/AddStore")]
        public async Task<IActionResult> AddStoreAsync([FromBody] StoreDTO storeDto)
        {
            try
            {
                return Ok(await _storeService.AddStoreAsync(storeDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

        [HttpPost("/AddChainOfStore")]
        public async Task<IActionResult> AddChainOfStoreAsync([FromBody] ChainOfStoreDTO chainStoreDto)
        {
            try
            {
                return Ok(await _chainOfStoresService.AddChainOfStoreAsync(chainStoreDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

        [HttpPut("/UpdateStore")]
        public async Task<IActionResult> UpdateStoreAsync([FromBody] StoreDTO storeDto)
        {
            try
            {
                return Ok(await _storeService.UpdateStoreAsync(storeDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("/UpdateChainOfStore")]
        public async Task<IActionResult> UpdateChainOfStoreAsync([FromBody] ChainOfStoreDTO chainStoreDto)
        {
            try
            {
                return Ok(await _chainOfStoresService.UpdateChainOfStoreAsync(chainStoreDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("/DeleteStore/{storeId}")]
        public async Task<IActionResult> DeleteStoreAsync(int storeId)
        {
            try
            {
                return Ok(await _storeService.DeleteStoreAsync(storeId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("/DeleteChainOfStore/{chainStoreId}")]
        public async Task<IActionResult> DeleteChainOfStoreAsync(int chainStoreId)
        {
            try
            {
                return Ok(await _chainOfStoresService.DeleteChainOfStoreAsync(chainStoreId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
