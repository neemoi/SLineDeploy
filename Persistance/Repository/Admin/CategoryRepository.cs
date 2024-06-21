using Application.DtoModels.Models.Admin;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Persistance.Context;

namespace Application.Services.Interfaces.IRepository.Admin
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreLineContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(StoreLineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _context.Categories.ToListAsync();

                return _mapper.Map<List<Category>>(categories);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in CategoryRepository -> GetAllCategoriesAsync: {ex.Message}");
            }
        }

        public async Task<Category> AddCategoryAsync(CategoryDTO categoryDto)
        {
            try
            {
                var existingCategory = await _context.Categories
                    .FirstOrDefaultAsync(c => c.CategoryName == categoryDto.CategoryName);
                
                if (existingCategory != null)
                {
                    throw new InvalidOperationException($"A category named {categoryDto.CategoryName} already exists");
                }
                
                var category = _mapper.Map<Category>(categoryDto);

                _context.Categories.Add(category);

                await _context.SaveChangesAsync();

                return category;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in CategoryRepository -> AddCategoryAsync: {ex.Message}");
            }
        }

        public async Task<Category> UpdateCategoryAsync(CategoryDTO categoryDto)
        {
            try
            {
                var category = await _context.Categories.FindAsync(categoryDto.CategoryId)
                    ?? throw new KeyNotFoundException($"The category with ID {categoryDto.CategoryId} was not found");

                if (!string.IsNullOrEmpty(categoryDto.CategoryName) && categoryDto.CategoryName != category.CategoryName)
                {
                    var otherCategory = await _context.Categories
                        .FirstOrDefaultAsync(c => c.CategoryName == categoryDto.CategoryName && c.CategoryId != categoryDto.CategoryId);

                    if (otherCategory != null)
                    {
                        throw new InvalidOperationException($"Another category named {categoryDto.CategoryName} already exists");
                    }
                }

                if (!string.IsNullOrEmpty(categoryDto.CategoryImage))
                {
                    category.CategoryImage = categoryDto.CategoryImage;
                }

                if (!string.IsNullOrEmpty(categoryDto.CategoryName))
                {
                    category.CategoryName = categoryDto.CategoryName;
                }

                await _context.SaveChangesAsync();

                return category;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in CategoryRepository -> UpdateCategoryAsync: {ex.Message}");
            }
        }

        public async Task<Category> DeleteCategoryAsync(int categoryId)
        {
            try
            {
                var category = await _context.Categories.FindAsync(categoryId)
                    ?? throw new KeyNotFoundException($"Category with ID {categoryId} not found");
                   
                _context.Categories.Remove(category);

                await _context.SaveChangesAsync();

                return category;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in CategoryRepository -> DeleteCategoryAsync: {ex.Message}");
            }
        }
    }
}
