using Persistance;

namespace Domain.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public string? Type { get; set; }

        public decimal Commission { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
