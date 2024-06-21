using Application.DtoModels.Models.User.Order;
using Persistance;

namespace Application.Services.Interfaces.IRepository.User
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(CreateOrder model);

        Task<List<Order>> GetOrdersByUserIdAsync(string userId);

        Task<List<DeliveryOptionDto>> GetDeliveryAsync(int storeId);

        Task<List<OrderStatusDto>> GetOrderStatusAsync();

        Task<List<PaymentDto>> GetPaymentTypeAsync();

        Task<Order> UpdateOrderStatusAsync(int orderId, int statusId);

        Task<Order> CancelOrderAsync(int orderId, string userId);
    }
}
