namespace Application.DtoModels.Response.User.Product
{
    public class ProductResponseDto
    {
        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public string? Description { get; set; }

        public string? Manufacturer { get; set; }

        public string? Composition { get; set; }

        public string? StorageConditions { get; set; }

        public int? ShelfLife { get; set; }

        public int? Calories { get; set; }

        public decimal? Proteins { get; set; }

        public decimal? Fats { get; set; }

        public decimal? Carbohydrates { get; set; }

        public int SubcategoryId { get; set; }

        public string? SubcategoryName { get; set; }

        public byte[]? Image { get; set; }
    }
}
