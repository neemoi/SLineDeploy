using Application.DtoModels.Models.User.Order;
using Application.DtoModels.Response.User.Order;
using Application.Services.Interfaces.IServices.User;
using Application.UnitOfWork;
using AutoMapper;
using Persistance;

namespace Application.Services.Implementations.User
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderResponseDto> CancelOrderAsync(int orderId, string userId)
        {
            try
            {
                var result = await _unitOfWork.OrderRepository.CancelOrderAsync(orderId, userId);

                return _mapper.Map<OrderResponseDto>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in OrderService -> CancelOrderAsync: {ex.Message}");
            }
        }

        public async Task<OrderResponseDto> CreateOrderAsync(CreateOrder model)
        {
            try
            {
                var result = await _unitOfWork.OrderRepository.CreateOrderAsync(model);

                return _mapper.Map<OrderResponseDto>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in OrderService -> CreateOrderAsync: {ex.Message}");
            }
        }

        public async Task<List<OrderResponseDto>> GetOrdersByUserIdAsync(string userId)
        {
            try
            {
                var result = await _unitOfWork.OrderRepository.GetOrdersByUserIdAsync(userId);

                return _mapper.Map<List<OrderResponseDto>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in OrderService -> GetOrdersByUserIdAsync: {ex.Message}");
            }
        }

        public async Task<List<DeliveryOptionDto>> GetDeliveryAsync(int storeId)
        {
            try
            {
                var result = await _unitOfWork.OrderRepository.GetDeliveryAsync(storeId);

                return _mapper.Map<List<DeliveryOptionDto>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in OrderService -> GetAllDataDeliveryAndPaymentsAsync: {ex.Message}");
            }
        }

        public async Task<List<OrderStatusDto>> GetOrderStatusAsync()
        {
            try
            {
                var result = await _unitOfWork.OrderRepository.GetOrderStatusAsync();

                return _mapper.Map<List<OrderStatusDto>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in OrderService -> GetOrderStatus: {ex.Message}");
            }
        }

        public async Task<List<PaymentDto>> GetPaymentTypeAsync()
        {
            try
            {
                var result = await _unitOfWork.OrderRepository.GetPaymentTypeAsync();

                return _mapper.Map<List<PaymentDto>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in OrderService -> GetOrderStatus: {ex.Message}");
            }
        }

        public async Task<UpdateOrderStatusDto> UpdateOrderStatusAsync(int orderId, int statusId)
        {
            try
            {
                var result = await _unitOfWork.OrderRepository.UpdateOrderStatusAsync(orderId, statusId);

                return _mapper.Map<UpdateOrderStatusDto>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in OrderService -> UpdateOrderStatusAsync: {ex.Message}");
            }
        }
    }
}
