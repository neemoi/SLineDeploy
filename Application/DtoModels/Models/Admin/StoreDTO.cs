namespace Application.DtoModels.Models.Admin
{
    public class StoreDTO
    {
        public int StoreId { get; set; }

        public string? StoreName { get; set; }

        public string? City { get; set; }

        public string? Address { get; set; }

        public int? ChainId { get; set; }
    }    
}
