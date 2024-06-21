namespace Persistance;

public partial class DeliveryOption
{
    public int DeliveryId { get; set; }

    public int? DeliveryTime { get; set; }
    
    public string? DeliveryType { get; set; }

    public decimal? DeliveryPrice { get; set; }

    public int? StoreId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Store? Store { get; set; }
}
