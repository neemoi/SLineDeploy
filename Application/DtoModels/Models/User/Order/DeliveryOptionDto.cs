namespace Application.DtoModels.Models.User.Order
{
    public class DeliveryOptionDto
    {
        public int DeliveryId { get; set; }

        public int DeliveryTime { get; set; }

        public string? DeliveryType { get; set; }

        public decimal DeliveryPrice { get; set; }

        public int StoreId { get; set; }
    }
}
