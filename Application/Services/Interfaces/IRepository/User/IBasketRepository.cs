using Application.DtoModels.Models.User.Cart;
using Persistance;

namespace Application.Services.Interfaces.IRepository.User
{
    public interface IBasketRepository
    {
        Task<UserCart> AddProductToBasketAsync(BasketDto model);

        Task<List<UserCart>> GetBasketItemsAsync(string userId);

        Task<List<Warehouse>> GetProductsAvailableStores(int productId);

        Task<UserCart> UpdateBasketItemQuantityAsync(UpdateBasketItemDto model);

        Task<UserCart> RemoveProductBasketAsync(DeleteBasketProductDto model);

        Task<List<UserCart>> RemoveAllUserBasketAsync(string userId);
    }
}
