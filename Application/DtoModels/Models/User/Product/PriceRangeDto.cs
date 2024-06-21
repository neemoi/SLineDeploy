namespace Application.DtoModels.Models.User.Product
{
    public class PriceRangeDto
    {
        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }

        public int MinQuantity {  get; set; }

        public int MaxQuantity {  get; set; }
    }
}
