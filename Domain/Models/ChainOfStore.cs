namespace Persistance;

public partial class ChainOfStore
{
    public int ChainId { get; set; }

    public string? ChainName { get; set; }

    public string? Description { get; set; }

    public string? ChainImage { get; set; }

    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
