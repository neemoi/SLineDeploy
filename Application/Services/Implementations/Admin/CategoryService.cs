using Application.DtoModels.Models.Admin;
using Application.Services.Interfaces;
using Application.UnitOfWork;
using AutoMapper;

namespace Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
    {
        try
        {
            var result = await _unitOfWork.CategoryRepository.GetAllCategoriesAsync();
            
            return _mapper.Map<List<CategoryDTO>>(result);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error in CategoryService -> GetAllCategoriesAsync: {ex.Message}");
        }
    }

    public async Task<CategoryDTO> AddCategoryAsync(CategoryDTO categoryDto)
    {
        try
        {
            var result = await _unitOfWork.CategoryRepository.AddCategoryAsync(categoryDto);

            return _mapper.Map<CategoryDTO>(result);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error in CategoryService -> AddCategoryAsync: {ex.Message}");
        }
    }

    public async Task<CategoryDTO> UpdateCategoryAsync(CategoryDTO categoryDto)
    {
        try
        {
            var category = await _unitOfWork.CategoryRepository.UpdateCategoryAsync(categoryDto);

            return _mapper.Map<CategoryDTO>(category);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error in CategoryService -> UpdateCategoryAsync: {ex.Message}");
        }
    }

    public async Task<CategoryDTO> DeleteCategoryAsync(int categoryId)
    {
        try
        {
            var category = await _unitOfWork.CategoryRepository.DeleteCategoryAsync(categoryId);

            return _mapper.Map<CategoryDTO>(category);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error in CategoryService -> DeleteCategoryAsync: {ex.Message}");
        }
    }
}
