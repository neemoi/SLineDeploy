namespace Application.DtoModels.Response.User.Product
{
    public class GetProductsStoresResponseDto
    {
        public int StoreId { get; set; }

        public int ProductId { get; set; }

        public string? StoreName { get; set; }

        public string? City { get; set; }

        public string? Address { get; set; }

        public string? ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal ProductPrice { get; set; }

        public int DeliveryTime { get; set; }

        public decimal DeliveryPrice { get; set; }

        public string? DeliveryType { get; set; }
    }
}
