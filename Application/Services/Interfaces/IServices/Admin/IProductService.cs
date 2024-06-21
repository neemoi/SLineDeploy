using Application.DtoModels.Models.Admin;

namespace Application.Services.Interfaces.IServices.Admin
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllProductsAsync();

        Task<ProductDTO> AddProductAsync(ProductDTO productDto);

        Task<ProductDTO> UpdateProductAsync(ProductDTO productDto);

        Task<ProductDTO> DeleteProductAsync(int productId);
    }
}
