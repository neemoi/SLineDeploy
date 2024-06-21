using Application.DtoModels.Models.Admin;
using Application.Services.Interfaces.IServices.Admin;
using Application.UnitOfWork;
using AutoMapper;

namespace Application.Services.Implementations.Admin
{
    public class ChainOfStoreService : IChainOfStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ChainOfStoreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ChainOfStoreDTO> AddChainOfStoreAsync(ChainOfStoreDTO chainStoreDto)
        {
            try
            {
                var result = await _unitOfWork.ChainOfStoresRepository.AddChainOfStoreAsync(chainStoreDto);

                return _mapper.Map<ChainOfStoreDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ChainOfStoreService -> AddChainOfStorAsync: {ex.Message}");
            }
        }

        public async Task<ChainOfStoreDTO> DeleteChainOfStoreAsync(int chainStoreId)
        {
            try
            {
                var result = await _unitOfWork.ChainOfStoresRepository.DeleteChainOfStoreAsync(chainStoreId);

                return _mapper.Map<ChainOfStoreDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ChainOfStoreService -> DeleteChainOfStorAsync: {ex.Message}");
            }
        }

        public async Task<List<ChainOfStoreDTO>> GetAllChainOfStoreAsync()
        {
            try
            {
                var result = await _unitOfWork.ChainOfStoresRepository.GetAllChainOfStoreAsync();

                return _mapper.Map<List<ChainOfStoreDTO>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ChainOfStoreService -> GetAllChainOfStoreAsync: {ex.Message}");
            }
        }

        public async Task<ChainOfStoreDTO> UpdateChainOfStoreAsync(ChainOfStoreDTO chainStoreDto)
        {
            try
            {
                var result = await _unitOfWork.ChainOfStoresRepository.UpdateChainOfStoreAsync(chainStoreDto);

                return _mapper.Map<ChainOfStoreDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ChainOfStoreService -> UpdateChainOfStorAsync: {ex.Message}");
            }
        }
    }
}
