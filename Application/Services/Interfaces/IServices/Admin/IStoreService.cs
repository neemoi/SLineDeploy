using Application.DtoModels.Models.Admin;

namespace Application.Services.Interfaces.IServices.Admin
{
    public interface IStoreService
    {
        Task<List<StoreDTO>> GetAllStoreAsync();

        Task<StoreDTO> AddStoreAsync(StoreDTO storeDto);

        Task<StoreDTO> UpdateStoreAsync(StoreDTO storeDto);

        Task<StoreDTO> DeleteStoreAsync(int storeId);
    }
}
