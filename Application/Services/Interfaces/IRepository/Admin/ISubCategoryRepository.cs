using Application.DtoModels.Models.Admin;
using Persistance;

namespace Application.Services.Interfaces.IRepository.Admin
{
    public interface ISubCategoryRepository
    {
        Task<List<Subcategory>> GetAllSubCategoriesAsync();

        Task<Subcategory> AddSubCategoryAsync(SubCategoryDTO subCategoryDto);

        Task<Subcategory> UpdateSubCategoryAsync(SubCategoryDTO subCategoryDto);

        Task<Subcategory> DeleteSubCategoryAsync(int subcategoryId);
    }
}
