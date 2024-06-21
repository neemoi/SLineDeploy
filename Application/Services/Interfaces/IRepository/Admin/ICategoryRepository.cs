using Application.DtoModels.Models.Admin;
using Persistance;

namespace Application.Services.Interfaces.IRepository.Admin
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoriesAsync();

        Task<Category> AddCategoryAsync(CategoryDTO categoryDto);

        Task<Category> UpdateCategoryAsync(CategoryDTO categoryDto);

        Task<Category> DeleteCategoryAsync(int categoryId);
    }

}
