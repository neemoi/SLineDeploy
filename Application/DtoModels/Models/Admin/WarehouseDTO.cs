namespace Application.DtoModels.Models.Admin
{
    public class WarehouseDTO
    {
        public int WarehouseId { get; set; }

        public int? StoreId { get; set; }

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }

        public decimal? ProductPrice { get; set; }
        public string? StoreName { get; set; }
    }
}
