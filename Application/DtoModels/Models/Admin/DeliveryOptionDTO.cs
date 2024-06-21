namespace Application.DtoModels.Models.Admin
{
    public class DeliveryOptionDTO
    {
        public int DeliveryId { get; set; }

        public int DeliveryTime { get; set; }

        public decimal DeliveryPrice { get; set; }

        public int StoreId { get; set; }
        
        public string? DeliveryType { get; set; }
    }
}
