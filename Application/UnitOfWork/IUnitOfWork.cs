using Application.Services.Interfaces.IRepository.Admin;
using Application.Services.Interfaces.IRepository.User;

namespace Application.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IInformationAboutStoresRepository InformationAboutStoresRepository { get; } 

        public ICatalogRepository CatalogRepository { get; }

        public IBasketRepository BasketRepository { get; }

        public IOrderRepository OrderRepository { get; }

        public IProfileRepository ProfileRepository { get; }

        public ICategoryRepository CategoryRepository { get; }

        public ISubCategoryRepository SubCategoryRepository { get; }

        public IDeliveryOptionRepository DeliveryOptionRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IWarehouseRepository WarehouseRepository { get; }

        public IStoreRepository StoreRepository { get; }

        public IChainOfStoreRepository ChainOfStoresRepository { get; }

        public IUserRepository UserRepository { get; }

        Task SaveChangesAsync();
    }
}
