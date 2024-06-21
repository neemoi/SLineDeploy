using Persistance;

namespace Application.Services.Interfaces.IRepository.User
{
    public interface IInformationAboutStoresRepository
    {
        Task<List<ChainOfStore>> GetAllChainsAsync();

        Task<List<Store>> GetAllStoresAsync();
    }
}
