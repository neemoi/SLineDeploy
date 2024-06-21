using Application.DtoModels.Models.Admin;
using Application.Services.Interfaces.IServices.Admin;
using Application.UnitOfWork;
using AutoMapper;

namespace Application.Services.Implementations.Admin
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDTO> AddProductAsync(ProductDTO productDto)
        {
            try
            {
                var result = await _unitOfWork.ProductRepository.AddProductAsync(productDto);

                return _mapper.Map<ProductDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ProductService -> AddProductAsync: {ex.Message}");
            }
        }

        public async Task<ProductDTO> DeleteProductAsync(int productId)
        {
            try
            {
                var result = await _unitOfWork.ProductRepository.DeleteProductAsync(productId);

                return _mapper.Map<ProductDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ProductService -> DeleteProductAsync: {ex.Message}");
            }
        }

        public async Task<List<ProductDTO>> GetAllProductsAsync()
        {
            try
            {
                var result = await _unitOfWork.ProductRepository.GetAllProductsAsync();

                return _mapper.Map<List<ProductDTO>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ProductService -> GetAllProductsAsync: {ex.Message}");
            }
        }

        public async Task<ProductDTO> UpdateProductAsync(ProductDTO productDto)
        {
            try
            {
                var result = await _unitOfWork.ProductRepository.UpdateProductAsync(productDto);

                return _mapper.Map<ProductDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ProductService -> UpdateProductAsync: {ex.Message}");
            }
        }
    }
}
