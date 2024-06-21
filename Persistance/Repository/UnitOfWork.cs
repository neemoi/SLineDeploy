using Application.Services.Interfaces.IRepository.Admin;
using Application.Services.Interfaces.IRepository.User;
using Application.UnitOfWork;
using Persistance.Context;

namespace Persistance.Repository
{
    public class UnitOfWork : IUnitOfWork
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

        private readonly StoreLineContext _storeLineContext;

        public UnitOfWork(StoreLineContext storeLineContext, IInformationAboutStoresRepository informationAboutStoresRepositoryStoreRepository, 
            ICatalogRepository catalogRepository, IBasketRepository basketRepository, IOrderRepository orderRepository, 
            IProfileRepository profileRepository, ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository,
            IDeliveryOptionRepository deliveryOptionRepository, IProductRepository productRepository,
            IWarehouseRepository warehouseRepository, IStoreRepository storeRepository,
            IChainOfStoreRepository chainOfStoresRepository, IUserRepository userRepository)
        {
            _storeLineContext = storeLineContext;
            InformationAboutStoresRepository = informationAboutStoresRepositoryStoreRepository;
            CatalogRepository = catalogRepository;
            BasketRepository = basketRepository;
            OrderRepository = orderRepository;
            ProfileRepository = profileRepository;
            CategoryRepository = categoryRepository;
            SubCategoryRepository = subCategoryRepository;
            DeliveryOptionRepository = deliveryOptionRepository;
            ProductRepository = productRepository;
            WarehouseRepository = warehouseRepository;
            StoreRepository = storeRepository;
            ChainOfStoresRepository = chainOfStoresRepository;
            UserRepository = userRepository;
        }

        public async Task SaveChangesAsync()
        {
            await _storeLineContext.SaveChangesAsync();
        }
    }
}
