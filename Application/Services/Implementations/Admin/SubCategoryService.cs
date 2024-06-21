using Application.DtoModels.Models.Admin;
using Application.Services.Interfaces.IServices.Admin;
using Application.UnitOfWork;
using AutoMapper;

namespace Application.Services.Implementations.Admin
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SubCategoryDTO> AddSubCategoryAsync(SubCategoryDTO subCategoryDto)
        {
            try
            {
                var result = await _unitOfWork.SubCategoryRepository.AddSubCategoryAsync(subCategoryDto);

                return _mapper.Map<SubCategoryDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in SubCategoryService -> AddSubCategoryAsync: {ex.Message}");
            }
        }

        public async Task<SubCategoryDTO> DeleteSubCategoryAsync(int subcategoryId)
        {
            try
            {
                var result = await _unitOfWork.SubCategoryRepository.DeleteSubCategoryAsync(subcategoryId);

                return _mapper.Map<SubCategoryDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in SubCategoryService -> DeleteSubCategoryAsync: {ex.Message}");
            }
        }

        public async Task<List<SubCategoryDTO>> GetAllSubCategoriesAsync()
        {
            try
            {
                var result = await _unitOfWork.SubCategoryRepository.GetAllSubCategoriesAsync();

                return _mapper.Map<List<SubCategoryDTO>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in SubCategoryService -> GetAllSubCategoriesAsync: {ex.Message}");
            }
        }

        public async Task<SubCategoryDTO> UpdateSubCategoryAsync(SubCategoryDTO subCategoryDto)
        {
            try
            {
                var result = await _unitOfWork.SubCategoryRepository.UpdateSubCategoryAsync(subCategoryDto);

                return _mapper.Map<SubCategoryDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in SubCategoryService -> UpdateSubCategoryAsync: {ex.Message}");
            }
        }
    }
}
