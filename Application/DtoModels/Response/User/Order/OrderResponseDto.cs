namespace Application.DtoModels.Response.User.Order
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }

        public string? StoreName { get; set; }

        public string? StoreCity { get; set; }

        public string? StoreAddress { get; set; }

        public string? ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime OrderDate { get; set; }

        public string? DeliveryType { get; set; }

        public decimal DeliveryPrice { get; set; }

        public int DeliveryTime { get; set; }

        public string? StatusName { get; set; }

        public string? PaymentType { get; set; }

        public decimal Commission { get; set; }

        public string? UserName { get; set; }

        public string? UserAddress { get; set; }

        public string? UserPhone { get; set; }

    }
}
