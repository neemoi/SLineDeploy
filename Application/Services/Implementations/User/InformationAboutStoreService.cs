using Application.DtoModels.Response.User.Store;
using Application.Services.Interfaces.IServices.User;
using Application.UnitOfWork;
using AutoMapper;

namespace Application.Services.Implementations.User
{
    public class InformationAboutStoreService : IInformationAboutStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InformationAboutStoreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ChainOfStoreResponseDto>> GetAllChainsAsync()
        {
            try
            {
                var result = await _unitOfWork.InformationAboutStoresRepository.GetAllChainsAsync(); 

                return _mapper.Map<List<ChainOfStoreResponseDto>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in StoreService -> GetAllChainsAsync: {ex.Message}");
            }
        }

        public async Task<List<StoreResponseDto>> GetAllStoresAsync()
        {
            try
            {
                var result = await _unitOfWork.InformationAboutStoresRepository.GetAllStoresAsync();

                return _mapper.Map<List<StoreResponseDto>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in StoreService -> GetAllStoresAsync: {ex.Message}");
            }
        }
    }
}
