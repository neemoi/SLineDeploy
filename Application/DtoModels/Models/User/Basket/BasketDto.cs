namespace Application.DtoModels.Models.User.Cart
{
    public class BasketDto
    {
        public string? UserId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public int StoreId { get; set; }
    }
}
