using Application.DtoModels.Models.Admin;
using Application.Services.Interfaces.IServices.Admin;
using Application.UnitOfWork;
using AutoMapper;

namespace Application.Services.Implementations.Admin
{
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StoreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<StoreDTO> AddStoreAsync(StoreDTO storeDto)
        {
            try
            {
                var result = await _unitOfWork.StoreRepository.AddStoreAsync(storeDto);

                return _mapper.Map<StoreDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in StoreService -> AddStoreAsync: {ex.Message}");
            }
        }

        public async Task<StoreDTO> DeleteStoreAsync(int storeId)
        {
            try
            {
                var result = await _unitOfWork.StoreRepository.DeleteStoreAsync(storeId);

                return _mapper.Map<StoreDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in StoreService -> DeleteStoreAsync: {ex.Message}");
            }
        }

        public async Task<List<StoreDTO>> GetAllStoreAsync()
        {
            try
            {
                var result = await _unitOfWork.StoreRepository.GetAllStoreAsync();

                return _mapper.Map<List<StoreDTO>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in StoreService -> GetAllStoreAsync: {ex.Message}");
            }
        }

        public async Task<StoreDTO> UpdateStoreAsync(StoreDTO storeDto)
        {
            try
            {
                var result = await _unitOfWork.StoreRepository.UpdateStoreAsync(storeDto);

                return _mapper.Map<StoreDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in StoreService -> UpdateStoreAsync: {ex.Message}");
            }
        }
    }
}
