using Application.DtoModels.Models.User.Order;
using Application.DtoModels.Response.User.Order;

namespace Application.Services.Interfaces.IServices.User
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateOrderAsync(CreateOrder model);

        Task<List<DeliveryOptionDto>> GetDeliveryAsync(int storeId);

        Task<List<OrderStatusDto>> GetOrderStatusAsync();

        Task<List<PaymentDto>> GetPaymentTypeAsync();
        
        Task<UpdateOrderStatusDto> UpdateOrderStatusAsync(int orderId, int statusId);

        Task<List<OrderResponseDto>> GetOrdersByUserIdAsync(string userId);

        Task<OrderResponseDto> CancelOrderAsync(int orderId, string userId);
    }
}
