using Application.Services.Interfaces.IServices.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistance;

namespace StoreLineAPI.Controllers.User
{
    [ApiController]
    [Route("/Catalog")]
    //[Authorize]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet("Categories")]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            try
            {
                return Ok(await _catalogService.GetCategoriesAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Warehouse/{productId}")]
        public async Task<IActionResult> GetPriceRangeByProductIdAsync(int productId)
        {
            try
            {
                return Ok(await _catalogService.GetPriceRangeByProductIdAsync(productId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Warehouse/{storeId:int}/{productId:int}")]
        public async Task<IActionResult> GetWarehouseDetailsAsync(int storeId, int productId)
        {
            try
            {
                return Ok(await _catalogService.GetWarehouseDetailsAsync(storeId, productId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error in WarehouseController -> GetWarehouseDetails: {ex.Message}" });
            }
        }

        [HttpGet("Categories/{categoryId}")]
        public async Task<IActionResult> GetSubcategoriesByCategoryIdAsync(int categoryId)
        {
            try
            {
                return Ok(await _catalogService.GetSubcategoriesByIdAsync(categoryId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Subcategories/{subcategoryId}")]
        public async Task<IActionResult> GetProductsBySubcategoryIdAsync(int subcategoryId)
        {
            try
            {
                return Ok(await _catalogService.GetProductsBySubcategoryIdAsync(subcategoryId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Products")]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            try
            {
                return Ok(await _catalogService.GetAllProductsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Products/{productName}")]
        public async Task<IActionResult> GetProductByNameAsync(string productName)
        {
            try
            {
                return Ok(await _catalogService.GetProductsByNameAsync(productName));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Product/{productId}")]
        public async Task<IActionResult> GetProductsByIdAsync(int productId)
        {
            try
            {
                return Ok(await _catalogService.GetProductsByIdAsync(productId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
