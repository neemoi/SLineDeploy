using Application.DtoModels.Models.Admin;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace StoreLineAPI.Controllers.Admin
{
    [ApiController]
    [Route("Admin/Category")]
    public class AdminCategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public AdminCategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("/GetAllCategories")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            try
            {
                return Ok(await _categoryService.GetAllCategoriesAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

        [HttpPost("/AddCategory")]
        public async Task<IActionResult> AddCategoryAsync([FromBody] CategoryDTO categoryDto)
        {
            try
            {
                return Ok(await _categoryService.AddCategoryAsync(categoryDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

        [HttpPut("/UpdateCategory")]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] CategoryDTO categoryDto)
        {
            try
            {
                return Ok(await _categoryService.UpdateCategoryAsync(categoryDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("/DeleteCategory/{categoryId}")]
        public async Task<IActionResult> DeleteCategoryAsync(int categoryId)
        {
            try
            {
                return Ok(await _categoryService.DeleteCategoryAsync(categoryId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}