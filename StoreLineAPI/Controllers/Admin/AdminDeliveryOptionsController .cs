using Application.DtoModels.Models.Admin;
using Application.Services.Interfaces.IServices.Admin;
using Application.Services.Interfaces.IServices.User;
using Microsoft.AspNetCore.Mvc;

namespace StoreLineAPI.Controllers.Admin
{
    [ApiController]
    [Route("Admin/Delivery")]
    public class AdminDeliveryOptionsController : ControllerBase
    {
        private readonly IDeliveryOptionService _deliveryOptionService;
        private readonly IOrderService _orderService;

        public AdminDeliveryOptionsController(IDeliveryOptionService deliveryOptionService, IOrderService orderService)
        {
            _deliveryOptionService = deliveryOptionService;
            _orderService = orderService;
        }

        [HttpGet("/GetDeliveryType")]
        public async Task<IActionResult> GetDeliveryTypeAsync(int storeId)
        {
            try
            {
                return Ok(await _orderService.GetDeliveryAsync(storeId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

        [HttpPost("/AddDeliveryOption")]
        public async Task<IActionResult> AddDeliveryOptionAsync([FromBody] DeliveryOptionDTO deliveryOptionDto)
        {
            try
            {
                return Ok(await _deliveryOptionService.AddDeliveryOptionAsync(deliveryOptionDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

        [HttpPut("/UpdateDeliveryOption")]
        public async Task<IActionResult> UpdateDeliveryOptionAsync([FromBody] DeliveryOptionDTO deliveryOptionDto)
        {
            try
            {
                return Ok(await _deliveryOptionService.UpdateDeliveryOptionAsync(deliveryOptionDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("/DeleteDeliveryOption/{deliveryOptionId}")]
        public async Task<IActionResult> DeleteDeliveryOptionAsync(int deliveryOptionId)
        {
            try
            {
                return Ok(await _deliveryOptionService.DeleteDeliveryOptionAsync(deliveryOptionId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }
    }
}
