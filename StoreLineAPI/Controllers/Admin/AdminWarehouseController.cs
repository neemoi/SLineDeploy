using Application.DtoModels.Models.Admin;
using Application.Services.Interfaces.IServices.Admin;
using Microsoft.AspNetCore.Mvc;

namespace StoreLineAPI.Controllers.Admin
{
    [ApiController]
    [Route("Admin/Warehouse")]
    public class AdminWarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public AdminWarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpGet("/GetAllWarehouses")]
        public async Task<IActionResult> GetAllWarehousesAsync()
        {
            try
            {
                return Ok(await _warehouseService.GetAllWarehousesAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

        [HttpPost("/AddWarehouse")]
        public async Task<IActionResult> AddWarehouseAsync([FromBody] WarehouseDTO warehouseDTO)
        {
            try
            {
                return Ok(await _warehouseService.AddWarehouseAsync(warehouseDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

        [HttpPut("/UpdateWarehouse")]
        public async Task<IActionResult> UpdateWarehouseAsync([FromBody] WarehouseDTO warehouseDTO)
        {
            try
            {
                return Ok(await _warehouseService.UpdateWarehouseAsync(warehouseDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("/DeleteWarehouse/{warehouseId}")]
        public async Task<IActionResult> DeleteWarehouseAsync(int warehouseId)
        {
            try
            {
                return Ok(await _warehouseService.DeleteWarehouseAsync(warehouseId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}