using Application.DtoModels.Models.Admin;

namespace Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllCategoriesAsync();

        Task<CategoryDTO> AddCategoryAsync(CategoryDTO categoryDto);

        Task<CategoryDTO> UpdateCategoryAsync(CategoryDTO categoryDto);

        Task<CategoryDTO> DeleteCategoryAsync(int categoryId);
    }
}
