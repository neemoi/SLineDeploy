namespace Application.DtoModels.Models.User.Order
{
    public class PaymentDto
    {
        public int Id { get; set; }

        public string? Type { get; set; }

        public decimal Commission { get; set; }
    }
}
