using Application.DtoModels.Models.Admin;
using Application.Services.Interfaces.IRepository.Admin;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;

namespace Persistance.Repository.Admin
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly StoreLineContext _context;
        private readonly IMapper _mapper;

        public SubCategoryRepository(StoreLineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Subcategory>> GetAllSubCategoriesAsync()
        {
            try
            {
                var subcategories = await _context.Subcategories.ToListAsync();

                return _mapper.Map<List<Subcategory>>(subcategories);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in SubCategoryRepository -> GetAllSubCategoriesAsync: {ex.Message}");
            }
        }

        public async Task<Subcategory> AddSubCategoryAsync(SubCategoryDTO subCategoryDto)
        {
            try
            {
                var existingSubCategory = await _context.Subcategories
                    .FirstOrDefaultAsync(sc => sc.SubcategoryName == subCategoryDto.SubCategoryName);

                if (existingSubCategory != null)
                {
                    throw new InvalidOperationException($"A subcategory named '{subCategoryDto.SubCategoryName}' already exists.");
                }

                var existingCategory = await _context.Categories.FindAsync(subCategoryDto.CategoryId);
                if (existingCategory == null)
                {
                    throw new KeyNotFoundException($"Category with ID {subCategoryDto.CategoryId} does not exist.");
                }
                
                var subCategory = _mapper.Map<Subcategory>(subCategoryDto);

                _context.Subcategories.Add(subCategory);

                await _context.SaveChangesAsync();

                return subCategory;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in SubCategoryRepository -> AddSubCategoryAsync: {ex.Message}");
            }
        }

        public async Task<Subcategory> UpdateSubCategoryAsync(SubCategoryDTO subCategoryDto)
        {
            try
            {
                var subCategory = await _context.Subcategories.FindAsync(subCategoryDto.SubCategoryId)
                    ?? throw new KeyNotFoundException($"The subcategory with ID {subCategoryDto.SubCategoryId} was not found.");

                if (!string.IsNullOrEmpty(subCategoryDto.SubCategoryName) && subCategoryDto.SubCategoryName != subCategory.SubcategoryName)
                {
                    var otherSubcategory = await _context.Subcategories
                        .FirstOrDefaultAsync(sc => sc.SubcategoryName == subCategoryDto.SubCategoryName && sc.SubcategoryId != subCategoryDto.SubCategoryId);

                    if (otherSubcategory != null)
                    {
                        throw new InvalidOperationException($"Another subcategory named '{subCategoryDto.SubCategoryName}' already exists.");
                    }
                }

                if (subCategoryDto.CategoryId != 0 && subCategoryDto.CategoryId != subCategory.CategoryId)
                {
                    var categoryExists = await _context.Categories.FindAsync(subCategoryDto.CategoryId);
                    if (categoryExists == null)
                    {
                        throw new InvalidOperationException($"Category with ID {subCategoryDto.CategoryId} does not exist.");
                    }

                    subCategory.CategoryId = subCategoryDto.CategoryId;
                }

                if (!string.IsNullOrEmpty(subCategoryDto.SubCategoryName))
                {
                    subCategory.SubcategoryName = subCategoryDto.SubCategoryName;
                }

                if (!string.IsNullOrEmpty(subCategoryDto.SubCategoryImage))
                {
                    subCategory.SubcategoryImage = subCategoryDto.SubCategoryImage;
                }

                await _context.SaveChangesAsync();

                return subCategory;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in SubCategoryRepository -> UpdateSubCategoryAsync: {ex.Message}");
            }
        }

        public async Task<Subcategory> DeleteSubCategoryAsync(int subcategoryId)
        {
            try
            {
                var subCategory = await _context.Subcategories.FindAsync(subcategoryId)
                    ?? throw new KeyNotFoundException($"Subcategory with ID {subcategoryId} not found.");

                _context.Subcategories.Remove(subCategory);

                await _context.SaveChangesAsync();

                return subCategory; 
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in SubCategoryRepository -> DeleteCategoryAsync: {ex.Message}");
            }
        }
    }
}
