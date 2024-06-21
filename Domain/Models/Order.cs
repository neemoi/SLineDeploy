using Domain.Models;

namespace Persistance;

public partial class Order
{
    public int OrderId { get; set; }

    public int? CartId { get; set; }

    public int? PaymentId { get; set; }

    public int? DeliveryId { get; set; }

    public int? StatusId { get; set; }

    public DateTime? OrderDate { get; set; }

    public virtual UserCart? Cart { get; set; }

    public virtual DeliveryOption? Delivery { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual OrderStatus? Status { get; set; }

    public virtual Payment? Payment { get; set; }
}
