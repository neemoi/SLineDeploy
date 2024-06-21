using Application.DtoModels.Models.Admin;
using Application.Services.Interfaces.IRepository.Admin;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;

namespace Persistance.Repository.Admin
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreLineContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(StoreLineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Product> AddProductAsync(ProductDTO productDto)
        {
            var existingProduct = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductName == productDto.ProductName);

            if (existingProduct != null)
            {
                throw new InvalidOperationException($"Product with name '{productDto.ProductName}' already exists.");
            }

            if (productDto.SubcategoryId != 0)
            {
                var subcategory = await _context.Subcategories.FindAsync(productDto.SubcategoryId);

                if (subcategory == null)
                {
                    throw new InvalidOperationException($"Subcategory with ID {productDto.SubcategoryId} does not exist.");
                }
            }

            var product = _mapper.Map<Product>(productDto);

            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> DeleteProductAsync(int productId)
        {
            try
            {
                var product = await _context.Products.FindAsync(productId)
                    ?? throw new KeyNotFoundException($"Product with ID {productId} not found");

                _context.Products.Remove(product);

                await _context.SaveChangesAsync();

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ProductRepository -> DeleteProductAsync: {ex.Message}");
            }
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            try
            {
                var products = await _context.Products.ToListAsync();

                return _mapper.Map<List<Product>>(products);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ProductRepository -> GetAllProductsAsync: {ex.Message}");
            }
        }

        public async Task<Product> UpdateProductAsync(ProductDTO productDto)
        {
            try
            {
                var product = await _context.Products.FindAsync(productDto.ProductId)
                    ?? throw new KeyNotFoundException($"The product with ID {productDto.ProductId} was not found");

                if (!string.IsNullOrEmpty(productDto.ProductName))
                {
                    product.ProductName = productDto.ProductName;
                }

                if (!string.IsNullOrEmpty(productDto.Description))
                {
                    product.Description = productDto.Description;
                }

                if (!string.IsNullOrEmpty(productDto.Manufacturer))
                {
                    product.Manufacturer = productDto.Manufacturer;
                }

                if (!string.IsNullOrEmpty(productDto.Composition))
                {
                    product.Composition = productDto.Composition;
                }

                if (!string.IsNullOrEmpty(productDto.StorageConditions))
                {
                    product.StorageConditions = productDto.StorageConditions;
                }

                if (productDto.ShelfLife.HasValue)
                {
                    product.ShelfLife = productDto.ShelfLife.Value;
                }

                if (productDto.Calories.HasValue)
                {
                    product.Calories = productDto.Calories.Value;
                }

                if (productDto.Proteins.HasValue)
                {
                    product.Proteins = productDto.Proteins.Value;
                }

                if (productDto.Fats.HasValue)
                {
                    product.Fats = productDto.Fats.Value;
                }

                if (productDto.Carbohydrates.HasValue)
                {
                    product.Carbohydrates = productDto.Carbohydrates.Value;
                }

                if (productDto.SubcategoryId.HasValue)
                {
                    var subcategory = await _context.Subcategories.FindAsync(productDto.SubcategoryId.Value);
                    
                    if (subcategory == null)
                    {
                        throw new InvalidOperationException($"Subcategory with ID {productDto.SubcategoryId} does not exist.");
                    }
                    
                    product.SubcategoryId = productDto.SubcategoryId.Value;
                }

                if (productDto.Image != null)
                {
                    product.Image = productDto.Image;
                }

                await _context.SaveChangesAsync();

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ProductRepository -> UpdateProductAsync: {ex.Message}");
            }
        }
    }
}
