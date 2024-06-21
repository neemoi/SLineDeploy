using Application.DtoModels.Models.Admin;
using Persistance;

namespace Application.Services.Interfaces.IRepository.Admin
{
    public interface IStoreRepository
    {
        Task<List<Store>> GetAllStoreAsync();

        Task<Store> AddStoreAsync(StoreDTO storeDto);

        Task<Store> UpdateStoreAsync(StoreDTO storeDto);

        Task<Store> DeleteStoreAsync(int storeId);
    }
}
