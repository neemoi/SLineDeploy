using Application.DtoModels.Models.User.Cart;
using Application.Services.Interfaces.IServices.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StoreLineAPI.Controllers.User
{
    [ApiController]
    [Route("/Basket")]
    //[Authorize]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProductToBasketAsync(BasketDto model)
        {
            try
            {
                return Ok(await _basketService.AddProductToBasketAsync(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateQuantity")]
        public async Task<IActionResult> UpdateBasketItemQuantityAsync(UpdateBasketItemDto model)
        {
            try
            {
                return Ok(await _basketService.UpdateBasketItemQuantityAsync(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("BasketItems")]
        public async Task<IActionResult> GetBasketItemsAsync(string userId)
        {
            try
            {
                return Ok(await _basketService.GetBasketItemsAsync(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("AvailableStores/{productId}")]
        public async Task<IActionResult> GetProductsAvailableStoresAsync(int productId)
        {
            try
            {
                return Ok(await _basketService.GetProductsAvailableStores(productId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("RemoveProduct")]
        public async Task<IActionResult> RemoveProductBasketAsync(DeleteBasketProductDto model)
        {
            try
            {
                return Ok(await _basketService.RemoveProductBasketAsync(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("RemoveBasket/{userId}")]
        public async Task<IActionResult> RemoveAllUserBasketAsync(string userId)
        {
            try
            {
                return Ok(await _basketService.RemoveAllUserBasketAsync(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
