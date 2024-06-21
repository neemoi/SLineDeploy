using Application.DtoModels.Models.User.Category;

namespace Application.DtoModels.Response.User.CategorySbcategory
{
    public class CategoryResponseDto
    {
        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public string? CategoryImage { get; set; }

        public List<SubcategoryDto>? Subcategories { get; set; }
    }
}
