using Application.DtoModels.Models.Admin;
using Application.Services.Interfaces.IServices.Admin;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("Admin/Product")]
public class AdminProductController : ControllerBase
{
    private readonly IProductService _productService;

    public AdminProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("/GetAllProducts")]
    public async Task<IActionResult> GetAllProductsAsync()
    {
        try
        {
            return Ok(await _productService.GetAllProductsAsync());
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");

        }
    }

    [HttpPost("/AddProduct")]
    public async Task<IActionResult> AddProductAsync([FromBody] ProductDTO productDto)
    {
        try
        {
            return Ok(await _productService.AddProductAsync(productDto));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");

        }
    }

    [HttpPut("/UpdateProduct")]
    public async Task<IActionResult> UpdateProductAsync([FromBody] ProductDTO productDto)
    {
        try
        {
            return Ok(await _productService.UpdateProductAsync(productDto));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("/DeleteProduct/{productId}")]
    public async Task<IActionResult> DeleteProductAsync(int productId)
    {
        try
        {
            return Ok(await _productService.DeleteProductAsync(productId));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
