using Application.DtoModels.Models.Admin;

namespace Application.Services.Interfaces.IServices.Admin
{
    public interface IChainOfStoreService
    {
        Task<List<ChainOfStoreDTO>> GetAllChainOfStoreAsync();

        Task<ChainOfStoreDTO> AddChainOfStoreAsync(ChainOfStoreDTO chainStoreDto);

        Task<ChainOfStoreDTO> UpdateChainOfStoreAsync(ChainOfStoreDTO chainStoreDto);

        Task<ChainOfStoreDTO> DeleteChainOfStoreAsync(int chainStoreId);
    }
}
