using Application.DtoModels.Models.User.Cart;
using Application.DtoModels.Response.User.Basket;
using Application.DtoModels.Response.User.Product;

namespace Application.Services.Interfaces.IServices.User
{
    public interface IBasketService
    {
        Task<BasketResponseDto> AddProductToBasketAsync(BasketDto model);

        Task<List<BasketResponseDto>> GetBasketItemsAsync(string userId);

        Task<List<GetProductsStoresResponseDto>> GetProductsAvailableStores(int productId);

        Task<BasketResponseDto> UpdateBasketItemQuantityAsync(UpdateBasketItemDto model);

        Task<BasketResponseDto> RemoveProductBasketAsync(DeleteBasketProductDto model);

        Task<List<BasketResponseDto>> RemoveAllUserBasketAsync(string userId);
    }
}
