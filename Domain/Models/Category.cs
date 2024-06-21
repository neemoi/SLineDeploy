namespace Persistance;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string? CategoryImage { get; set; }

    public virtual ICollection<Subcategory> Subcategories { get; set; } = new List<Subcategory>();
}
