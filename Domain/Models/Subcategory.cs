namespace Persistance;

public partial class Subcategory
{
    public int SubcategoryId { get; set; }

    public string? SubcategoryName { get; set; }

    public string? SubcategoryImage { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
