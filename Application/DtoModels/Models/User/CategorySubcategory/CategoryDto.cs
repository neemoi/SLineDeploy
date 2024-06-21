namespace Application.DtoModels.Models.User.Category
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public string? CategoryImage { get; set; }

        public List<SubcategoryDto>? Subcategories { get; set; }
    }
}
