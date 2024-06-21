using Application.DtoModels.Models.User.Product;
using Application.Services.Interfaces.IRepository.User;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;

namespace Persistance.Repository.User
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly StoreLineContext _storeLineContext;
        private readonly IMapper _mapper;

        public CatalogRepository(StoreLineContext storeLineContext, IMapper mapper)
        {
            _storeLineContext = storeLineContext;
            _mapper = mapper;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            try
            {
                var products = await _storeLineContext.Products.ToListAsync();

                if (products != null)
                {
                    return products;
                }
                else
                {
                    throw new Exception($"Products not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in CatalogRepository -> GetAllProductsAsync: {ex.Message}");
            };
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            try
            {
                var categories = await _storeLineContext.Categories
                   .Include(c => c.Subcategories) 
                   .ToListAsync();

                if (categories != null)
                {
                    return categories;
                }
                else
                {
                    throw new Exception($"Categories not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in CatalogRepository -> GetCategoriesAsync: {ex.Message}");
            };
        }

        public async Task<PriceRangeDto> GetPriceRangeByProductIdAsync(int productId)
        {
            try
            {
                var data = await _storeLineContext.Warehouses
                    .Where(w => w.ProductId == productId && w.ProductPrice.HasValue)
                    .GroupBy(w => w.ProductId)
                    .Select(g => new PriceRangeDto
                    {
                        MinPrice = g.Min(w => w.ProductPrice) ?? 0,
                        MaxPrice = g.Max(w => w.ProductPrice) ?? 0,
                        MinQuantity = g.Min(w => w.Quantity) ?? 0,
                        MaxQuantity = g.Max(w => w.Quantity) ?? 0
                    })
                    .FirstOrDefaultAsync();

                if (data != null)
                {
                    return data;
                }
                else
                {
                    throw new Exception($"No data found for product with ID {productId}.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in CatalogRepository -> GetPriceRangeByProductIdAsync: {ex.Message}");
            }
        }

        public async Task<Warehouse> GetWarehouseDetailsAsync(int storeId, int productId)
        {
            try
            {
                var data = await _storeLineContext.Warehouses
                    .Where(w => w.StoreId == storeId && w.ProductId == productId)
                    .FirstOrDefaultAsync();

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in CatalogRepository -> GetWarehouseDetailsAsync: {ex.Message}");
            }
        }

        public async Task<Product> GetProductsByIdAsync(int productId)
        {
            try
            {
                var product = await _storeLineContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
                  
                if (product != null)
                {
                    return product;
                }
                else
                {
                    throw new Exception($"Product Id ({productId}) not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in CatalogRepository -> GetProductsByIdAsync: {ex.Message}");
            }
        }

        public async Task<List<Product>> GetProductsByNameAsync(string productName)
        {
            try
            {
                var products = await _storeLineContext.Products
                    .Where(p => p.ProductName.Contains(productName))
                    .ToListAsync();

                if (products != null)
                {
                    return products;
                }
                else
                {
                    throw new Exception($"Product name ({productName}) not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in CatalogRepository -> GetProductsByNameAsync: {ex.Message}");
            }
        }

        public async Task<List<Product>> GetProductsBySubcategoryIdAsync(int subcategoryId)
        {
            try
            {
                var products = await _storeLineContext.Products
                    .Include(c => c.Subcategory)
                    .Where(p => p.SubcategoryId == subcategoryId)
                    .ToListAsync();

                if (products != null)
                {
                    return products;
                }
                else
                {
                    throw new Exception($"Products with subcategoryId ({subcategoryId}) not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in CatalogRepository -> GetProductsBySubcategoryIdAsync: {ex.Message}");
            }
        }

        public async Task<List<Subcategory>> GetSubcategoriesByIdAsync(int categoryId)
        {
            try
            {
                var subcategories = await _storeLineContext.Subcategories
                    .Where(s => s.CategoryId == categoryId)
                    .ToListAsync();

                if (subcategories != null)
                {
                    return subcategories;
                }
                else
                {
                    throw new Exception($"Subcategory with categoryId ({categoryId}) not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in CatalogRepository -> GetSubcategoriesByIdAsync: {ex.Message}");
            }
        }
    }
}
