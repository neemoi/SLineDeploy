namespace Application.DtoModels.Response.User.Product
{
    public class PriceRangeResponseDto
    {
        public int Quantity { get; set; }

        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }

        public int MinQuantity { get; set; }

        public int MaxQuantity { get; set; }
    }
}
