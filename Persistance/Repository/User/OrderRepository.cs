using Application.DtoModels.Models.User.Order;
using Application.Services.Interfaces.IRepository.User;
using Application.Services.Interfaces.IServices.Admin;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;
using System.Text;

namespace Persistance.Repository.User
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreLineContext _storeLineContext;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public OrderRepository(StoreLineContext storeLineContext, IMapper mapper, IEmailService emailService)
        {
            _storeLineContext = storeLineContext;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<Order> CreateOrderAsync(CreateOrder model)
        {
            try
            {
                var user = await _storeLineContext.Users.FirstOrDefaultAsync(u => u.Id == model.UserId);

                if (user == null || string.IsNullOrEmpty(user.Address))
                {
                    throw new Exception($"User with id ({model.UserId}) has no address specified. Cannot create order. Add User address");
                }

                var userCart = await _storeLineContext.UserCarts
                    .Include(c => c.Product)
                    .Include(c => c.Store)
                    .Include(c => c.User)
                    .Include(c => c.Orders)
                        .ThenInclude(c => c.OrderItems)
                    .Include(c => c.Orders)
                        .ThenInclude(c => c.Status)
                    .Include(c => c.Orders)
                        .ThenInclude(c => c.Delivery)
                    .Include(c => c.Orders)
                        .ThenInclude(c => c.Payment)
                    .Where(c => c.UserId == model.UserId && c.StoreId == model.StoreId)
                    .ToListAsync();

                if (!userCart.Any())
                {
                    throw new Exception($"User id ({model.UserId}) has no items for store id ({model.StoreId}) in the cart.");
                }

                if (userCart.Any(c => !c.IsOrdered))
                {
                    var deliveryOptions = await _storeLineContext.DeliveryOptions
                        .Where(d => d.StoreId == model.StoreId)
                        .ToListAsync();

                    var hasDeliveryOption = deliveryOptions.Any(d => d.DeliveryId == model.DeliveryId);

                    if (!hasDeliveryOption)
                    {
                        throw new Exception($"Store with ID {model.StoreId} does not offer delivery option with ID {model.DeliveryId}.");
                    }

                    foreach (var cartItem in userCart)
                    {
                        var availableQuantity = await GetAvailableQuantity(cartItem.ProductId.Value, cartItem.StoreId.Value);

                        if (availableQuantity < cartItem.Quantity)
                        {
                            throw new Exception($"Not enough quantity of product with ID {cartItem.ProductId.Value} in the store with ID {cartItem.StoreId.Value}.");
                        }
                    }

                    var order = _mapper.Map<Order>(model);
                    order.OrderDate = DateTime.Now;
                    order.CartId = userCart.First().CartId;

                    foreach (var cartItem in userCart)
                    {
                        var orderItem = _mapper.Map<OrderItem>(cartItem);
                        orderItem.TotalPrice = cartItem.Price * cartItem.Quantity;
                        order.OrderItems.Add(orderItem);

                        await UpdateWarehouseQuantity(cartItem.ProductId.Value, cartItem.StoreId.Value, cartItem.Quantity.Value);
                    }

                    order.DeliveryId = model.DeliveryId;

                    _storeLineContext.Orders.Add(order);

                    await _storeLineContext.SaveChangesAsync();

                    foreach (var cartItem in userCart)
                    {
                        cartItem.IsOrdered = true;
                    }

                    await _storeLineContext.SaveChangesAsync();

                    if (user.Email != null)
                    {
                        StringBuilder message = new StringBuilder();
                        message.AppendLine($"<h1>Заказ номер #{order.OrderId}</h1>");
                        message.AppendLine("<table border='1' cellpadding='10'>");
                        message.AppendLine("<tr><th>Товар</th><th>Количество</th><th>Цена</th></tr>");

                        foreach (var orderItem in order.OrderItems)
                        {
                            message.AppendLine($"<tr><td>{orderItem.Product.ProductName}</td><td>{orderItem.Quantity} шт.</td><td>{orderItem.TotalPrice}р.</td></tr>");
                        }

                        message.AppendLine("</table>");

                        decimal totalPrice = order.OrderItems.Sum(item => item.TotalPrice ?? 0m);
                        message.AppendLine($"<p><strong>Способ доставки:</strong> {order.Delivery.DeliveryType}</p>");

                        if (order.Delivery.DeliveryType == "Пункт выдачи")
                        {
                            message.AppendLine($"<p><strong>Адрес пункта выдачи:</strong> {order.Delivery.Store.City}, {order.Delivery.Store.Address}</p>");
                        }
                        else if (order.Delivery.DeliveryType == "Курьер")
                        {
                            message.AppendLine($"<p><strong>Адрес доставки:</strong> {order.Cart.User.Address}</p>");
                            message.AppendLine($"<p><strong>Цена доставки:</strong> {order.Delivery.DeliveryPrice}р.</p>");
                        }

                        totalPrice += order.Delivery.DeliveryPrice ?? 0m; 

                        message.AppendLine($"<p><strong>Дата заказа:</strong> {order.OrderDate}</p>");
                        message.AppendLine($"<p><strong>Общая стоимость заказа:</strong> {totalPrice}р.</p>");

                        await _emailService.SendEmailAsync(user.Email, $"Заказ номер #{order.OrderId}", message.ToString());
                    }
                    else
                    {
                        throw new InvalidOperationException("User email is null or empty or invalid.");
                    }

                    return order;
                }
                else
                {
                    throw new Exception($"All items in the cart for user id ({model.UserId}) and store id ({model.StoreId}) are already ordered.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in OrderRepository -> CreateOrderAsync: {ex.Message}");
            }
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            try
            {
                var orders = await _storeLineContext.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                    .Include(o => o.Status)
                    .Include(o => o.Cart)
                        .ThenInclude(c => c.User)
                    .Include(o => o.Delivery)
                    .Include(o => o.Payment)
                    .Include(o => o.Cart)
                        .ThenInclude(c => c.Store)
                    .Where(o => o.OrderItems.Any(oi => oi.OrderId == o.OrderId && oi.Quantity > 0 && o.Cart.UserId == userId))
                    .ToListAsync();

                if (orders == null || !orders.Any())
                {
                    throw new Exception($"No orders found for user with id {userId}.");
                }

                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in OrderRepository -> GetOrdersByUserIdAsync: {ex.Message}");
            }
        }

        public async Task<List<DeliveryOptionDto>> GetDeliveryAsync(int storeId)
        {
            try
            {
                var delivery = await _storeLineContext.DeliveryOptions
               .Where(d => d.StoreId == storeId)
               .ToListAsync()
                ?? throw new Exception($"ID Store {storeId} not found.");

                return _mapper.Map<List<DeliveryOptionDto>>(delivery);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in OrderRepository -> GetDelivery: {ex.Message}");
            }
        }

        public async Task<List<OrderStatusDto>> GetOrderStatusAsync()
        {
            try
            {
                var orderStatus = await _storeLineContext.OrderStatuses
                .ToListAsync()
                ?? throw new Exception($"Statuses not found.");


                return _mapper.Map<List<OrderStatusDto>>(orderStatus);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in OrderRepository -> GetOrderStatus: {ex.Message}");
            }
        }

        public async Task<List<PaymentDto>> GetPaymentTypeAsync()
        {
            try
            {
                var payments = await _storeLineContext.Payments
                .ToListAsync()
                ?? throw new Exception($"Payments not found.");


                return _mapper.Map<List<PaymentDto>>(payments);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in OrderRepository -> GetOrderStatus: {ex.Message}");
            }
        }

        public async Task<Order> CancelOrderAsync(int orderId, string userId)
        {
            try
            {
                var order = await _storeLineContext.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                    .Include(o => o.Status)
                    .Include(o => o.Cart)
                        .ThenInclude(c => c.User)
                            .ThenInclude(u => u.UserCarts)
                    .Include(o => o.Delivery)
                    .Include(o => o.Payment)
                    .Include(o => o.Cart)
                        .ThenInclude(c => c.Store)
                    .FirstOrDefaultAsync(o => o.OrderId == orderId && o.Cart.UserId == userId)
                    ?? throw new Exception($"Order with ID {orderId} for user with ID {userId} not found.");

                if (DateTime.Now - order.OrderDate > TimeSpan.FromMinutes(10))
                {
                    throw new Exception($"Order with ID {orderId} cannot be cancelled as it has passed 10 minutes since its creation.");
                }

                if (order.Cart == null || order.Cart.Store == null)
                {
                    throw new Exception($"Failed to get user cart data or store data.");
                }

                foreach (var orderItem in order.OrderItems)
                {
                    await CancelUpdateWarehouseQuantity(orderItem.ProductId.Value, order.Cart.StoreId.Value, orderItem.Quantity.Value);
                }

                var userCartItems = await _storeLineContext.UserCarts
                    .Where(uc => uc.StoreId == order.Cart.StoreId && order.OrderItems.Select(oi => oi.ProductId).Contains(uc.ProductId))
                    .ToListAsync();

                foreach (var userCartItem in userCartItems)
                {
                    userCartItem.IsOrdered = false;
                }

                _storeLineContext.OrderItems.RemoveRange(order.OrderItems);

                _storeLineContext.Orders.Remove(order);

                await _storeLineContext.SaveChangesAsync();

                return order;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in OrderRepository -> CancelOrderAsync: {ex.Message}");
            }
        }

        public async Task<Order> UpdateOrderStatusAsync(int orderId, int statusId)
        {
            try
            {
                var order = await _storeLineContext.Orders
                    .FirstOrDefaultAsync(o => o.OrderId == orderId)
                    ?? throw new Exception($"Order with ID {orderId} not found.");

                order.StatusId = statusId;

                _storeLineContext.Orders.Update(order);

                await _storeLineContext.SaveChangesAsync();

                return order;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in OrderRepository -> UpdateOrderStatusAsync: {ex.Message}");
            }
        }

        private async Task<int> GetAvailableQuantity(int productId, int storeId)
        {
            var warehouse = await _storeLineContext.Warehouses.FirstOrDefaultAsync(w => w.ProductId == productId && w.StoreId == storeId);

            return warehouse != null ? warehouse.Quantity.GetValueOrDefault() : 0;
        }

        private async Task UpdateWarehouseQuantity(int productId, int storeId, int quantity)
        {
            var warehouse = await _storeLineContext.Warehouses.FirstOrDefaultAsync(w => w.ProductId == productId && w.StoreId == storeId);

            if (warehouse != null)
            {
                warehouse.Quantity -= quantity;

                await _storeLineContext.SaveChangesAsync();
            }
        }

        private async Task CancelUpdateWarehouseQuantity(int productId, int storeId, int quantity)
        {
            var warehouse = await _storeLineContext.Warehouses.FirstOrDefaultAsync(w => w.ProductId == productId && w.StoreId == storeId);

            if (warehouse != null)
            {
                warehouse.Quantity += quantity;

                await _storeLineContext.SaveChangesAsync();
            }
        }
    }
}
