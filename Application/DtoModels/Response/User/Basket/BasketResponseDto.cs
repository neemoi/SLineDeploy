namespace Application.DtoModels.Response.User.Basket
{
    public class BasketResponseDto
    {
        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int StoreId { get; set; }

        public string? StoreName { get; set; }

        public string? City { get; set; }

        public string? Address { get; set; }

        public string? ProductName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public bool IsOrdered { get; set; }

        public byte[]? Image { get; set; }
    }
}
