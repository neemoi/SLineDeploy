using Application.DtoModels.Models.Admin;
using Persistance;

namespace Application.Services.Interfaces.IRepository.Admin
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();

        Task<Product> AddProductAsync(ProductDTO productDto);

        Task<Product> UpdateProductAsync(ProductDTO productDto);

        Task<Product> DeleteProductAsync(int productId);
    }
}
