namespace Persistance;

public partial class Store
{
    public int StoreId { get; set; }

    public string? StoreName { get; set; }

    public string? City { get; set; }

    public string? Address { get; set; }

    public int? ChainId { get; set; }

    public virtual ChainOfStore? Chain { get; set; }

    public virtual ICollection<DeliveryOption> DeliveryOptions { get; set; } = new List<DeliveryOption>();

    public virtual ICollection<UserCart> UserCarts { get; set; } = new List<UserCart>();

    public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}
