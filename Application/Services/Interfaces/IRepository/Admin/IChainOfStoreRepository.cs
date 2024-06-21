using Application.DtoModels.Models.Admin;
using Persistance;

namespace Application.Services.Interfaces.IRepository.Admin
{
    public interface IChainOfStoreRepository
    {
        Task<List<ChainOfStore>> GetAllChainOfStoreAsync();

        Task<ChainOfStore> AddChainOfStoreAsync(ChainOfStoreDTO chainStoreDto);

        Task<ChainOfStore> UpdateChainOfStoreAsync(ChainOfStoreDTO chainStoreDto);

        Task<ChainOfStore> DeleteChainOfStoreAsync(int chainStoreId);
    }
}
