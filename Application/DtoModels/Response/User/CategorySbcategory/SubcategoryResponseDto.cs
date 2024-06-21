using Application.DtoModels.Models.User.Product;

namespace Application.DtoModels.Response.User.CategorySbcategory
{
    public class SubcategoryResponseDto
    {
        public int SubcategoryId { get; set; }

        public string? SubcategoryName { get; set; }

        public string? SubcategoryImage { get; set; }

        public int CategoryId { get; set; }

        public List<ProductDto>? Products { get; set; }
    }
}
