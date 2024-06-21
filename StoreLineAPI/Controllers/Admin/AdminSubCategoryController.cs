using Application.DtoModels.Models.Admin;
using Application.Services.Interfaces.IServices.Admin;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("Admin/SubCategory")]
public class AdminSubCategoryController : ControllerBase
{
    private readonly ISubCategoryService _subcategoryService;

    public AdminSubCategoryController(ISubCategoryService subcategoryService)
    {
        _subcategoryService = subcategoryService;
    }

    [HttpGet("/GetAllSubCategories")]
    public async Task<IActionResult> GetAllSubCategoriesAsync()
    {
        try
        {
            return Ok(await _subcategoryService.GetAllSubCategoriesAsync());
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");

        }
    }

    [HttpPost("/AddSubCategory")]
    public async Task<IActionResult> AddSubCategoryAsync([FromBody] SubCategoryDTO subcategoryDto)
    {
        try
        {
            return Ok(await _subcategoryService.AddSubCategoryAsync(subcategoryDto));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");

        }
    }

    [HttpPut("/UpdateSubCategory")]
    public async Task<IActionResult> UpdateSubCategoryAsync([FromBody] SubCategoryDTO subcategoryDto)
    {
        try
        {
            return Ok(await _subcategoryService.UpdateSubCategoryAsync(subcategoryDto));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("/DeleteSubCategory/{subcategoryId}")]
    public async Task<IActionResult> DeleteSubCategoryAsync(int subcategoryId)
    {
        try
        {
            return Ok(await _subcategoryService.DeleteSubCategoryAsync(subcategoryId));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}