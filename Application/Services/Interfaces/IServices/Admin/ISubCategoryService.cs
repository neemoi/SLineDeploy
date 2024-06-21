using Application.DtoModels.Models.Admin;

namespace Application.Services.Interfaces.IServices.Admin
{
    public interface ISubCategoryService
    {
        Task<List<SubCategoryDTO>> GetAllSubCategoriesAsync();

        Task<SubCategoryDTO> AddSubCategoryAsync(SubCategoryDTO subCategoryDto);

        Task<SubCategoryDTO> UpdateSubCategoryAsync(SubCategoryDTO subCategoryDto);

        Task<SubCategoryDTO> DeleteSubCategoryAsync(int subcategoryId);
    }
}
