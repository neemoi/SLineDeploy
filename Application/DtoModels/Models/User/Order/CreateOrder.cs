namespace Application.DtoModels.Models.User.Order
{
    public class CreateOrder
    {
        public string? UserId { get; set; }

        public int StoreId { get; set; }

        public int StatusId { get; set; }

        public int DeliveryId { get; set; }

        public int PaymentId { get; set; }
    }
}
