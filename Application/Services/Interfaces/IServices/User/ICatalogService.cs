using Application.DtoModels.Models.User.Product;
using Application.DtoModels.Response.User.CategorySbcategory;
using Application.DtoModels.Response.User.Product;

namespace Application.Services.Interfaces.IServices.User
{
    public interface ICatalogService
    {
        Task<List<CategoryResponseDto>> GetCategoriesAsync();

        Task<List<SubcategoryResponseDto>> GetSubcategoriesByIdAsync(int categoryId);

        Task<List<ProductResponseDto>> GetProductsBySubcategoryIdAsync(int subcategoryId);

        Task<PriceRangeResponseDto> GetPriceRangeByProductIdAsync(int productId);

        Task<WarehouseProductDto> GetWarehouseDetailsAsync(int storeId, int productId);

        Task<List<ProductResponseDto>> GetProductsByNameAsync(string productName);

        Task<ProductResponseDto> GetProductsByIdAsync(int productId);

        Task<List<ProductResponseDto>> GetAllProductsAsync();
    }
}
