namespace Persistance;

public partial class Warehouse
{
    public int WarehouseId { get; set; }

    public int? StoreId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? ProductPrice { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Store? Store { get; set; }
}
