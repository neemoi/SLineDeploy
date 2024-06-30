using Application.DtoModels.Models.Admin;
using Application.Services.Interfaces.IServices.Admin;
using Microsoft.AspNetCore.Mvc;
using System;

namespace StoreLineAPI.Controllers.Admin
{
    [ApiController]
    [Route("api/Admin/Warehouse")]
    public class AdminWarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public AdminWarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpGet("GetAllWarehouses")]
        public async Task<IActionResult> GetAllWarehousesAsync()
        {
            try
            {
                var warehouses = await _warehouseService.GetAllWarehousesAsync();
                return Ok(warehouses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddWarehouse")]
        public async Task<IActionResult> AddWarehouseAsync([FromBody] WarehouseDTO warehouseDTO)
        {
            try
            {
                var addedWarehouse = await _warehouseService.AddWarehouseAsync(warehouseDTO);
                return Ok(addedWarehouse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateWarehouse")]
        public async Task<IActionResult> UpdateWarehouseAsync([FromBody] WarehouseDTO warehouseDTO)
        {
            try
            {
                var updatedWarehouse = await _warehouseService.UpdateWarehouseAsync(warehouseDTO);
                return Ok(updatedWarehouse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteWarehouse/{warehouseId}")]
        public async Task<IActionResult> DeleteWarehouseAsync(int warehouseId)
        {
            try
            {
                var result = await _warehouseService.DeleteWarehouseAsync(warehouseId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
