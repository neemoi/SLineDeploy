using Application.DtoModels.Models.User.Cart;
using Application.Services.Interfaces.IRepository.User;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;

namespace Persistance.Repository.User
{
    public class BasketReporitory : IBasketRepository
    {
        private readonly StoreLineContext _storeLineContext;
        private readonly IMapper _mapper;

        public BasketReporitory(StoreLineContext storeLineContext, IMapper mapper)
        {
            _storeLineContext = storeLineContext;
            _mapper = mapper;
        }

        public async Task<UserCart> AddProductToBasketAsync(BasketDto model)
        {
            try
            {
                var store = await _storeLineContext.Stores.FirstOrDefaultAsync(s => s.StoreId == model.StoreId)
                    ?? throw new Exception($"Store ({model.StoreId}) not found");

                var product = await _storeLineContext.Products
                    .Include(p => p.Warehouses)
                    .FirstOrDefaultAsync(p => p.ProductId == model.ProductId)
                    ?? throw new Exception($"Product ({model.ProductId}) not found");

                var user = await _storeLineContext.Users.FirstOrDefaultAsync(u => u.Id == model.UserId)
                    ?? throw new Exception($"User ({model.UserId}) not found");

                if (string.IsNullOrEmpty(user.Address))
                {
                    throw new Exception($"User ({model.UserId}) does not have a delivery address set");
                }

                var warehouse = product.Warehouses.FirstOrDefault(w => w.StoreId == model.StoreId)
                    ?? throw new Exception($"Warehouse not found for store ID {model.StoreId}");
                
                if (warehouse.Quantity < model.Quantity)
                {
                    throw new Exception($"Insufficient quantity for product ID {model.ProductId} in store ID {model.StoreId}. Requested: {model.Quantity}, Available: {warehouse.Quantity}");
                }

                var existingCartItem = await _storeLineContext.UserCarts
                    .FirstOrDefaultAsync(c => c.UserId == model.UserId && c.ProductId == model.ProductId && c.StoreId == model.StoreId);

                if (existingCartItem != null)
                {
                    if (existingCartItem.IsOrdered)
                    {
                        if (model.Quantity <= 0)
                        {
                            throw new Exception($"Quantity of product with ID {model.ProductId} cannot be zero or negative.");
                        }

                        var newCartItem = _mapper.Map<UserCart>(model);
                        newCartItem.Price = product.Warehouses.FirstOrDefault()?.ProductPrice;
                        newCartItem.IsOrdered = false;

                        await _storeLineContext.UserCarts.AddAsync(newCartItem);

                        await _storeLineContext.SaveChangesAsync();

                        return newCartItem;
                    }
                    else
                    {
                        existingCartItem.Quantity += model.Quantity;

                        if (existingCartItem.Quantity <= 0)
                        {
                            throw new Exception($"Quantity of product with ID {model.ProductId} in the cart cannot be zero or negative.");
                        }

                        await _storeLineContext.SaveChangesAsync();

                        return existingCartItem;
                    }
                }
                else
                {
                    if (model.Quantity <= 0)
                    {
                        throw new Exception($"Quantity of product with ID {model.ProductId} cannot be zero or negative.");
                    }

                    var newCartItem = _mapper.Map<UserCart>(model);
                    newCartItem.Price = product.Warehouses.FirstOrDefault()?.ProductPrice;
                    newCartItem.IsOrdered = false;

                    await _storeLineContext.UserCarts.AddAsync(newCartItem);

                    await _storeLineContext.SaveChangesAsync();

                    return newCartItem;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in BasketReporitory -> AddProductToBasketAsync: {ex.Message}");
            }
        }

        public async Task<List<UserCart>> GetBasketItemsAsync(string userId)
        {
            try
            {
                var userExist = await _storeLineContext.Users
                    .Include(s => s.UserCarts)
                        .ThenInclude(s => s.Store)
                    .FirstOrDefaultAsync(i => i.Id == userId)
                    ?? throw new Exception($"User by id ({userId}) not found");

                var result = await _storeLineContext.UserCarts
                     .Where(c => c.UserId == userId)
                     .Include(p => p.Product)
                     .ToListAsync();

                if (result != null)
                {
                    return _mapper.Map<List<UserCart>>(result);
                }
                else
                {
                    throw new Exception($"User cart not found for UserId: ({userId})");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in BasketReporitory -> GetBasketItemsAsync: {ex.Message}");
            };
        }

        public async Task<List<Warehouse>> GetProductsAvailableStores(int productId)
        {
            try
            {
                var productExist = await _storeLineContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId)
                    ?? throw new Exception($"Product by id ({productId}) not found");

                var result = await _storeLineContext.Warehouses
                 .Include(w => w.Store)
                     .ThenInclude(s => s.DeliveryOptions)
                 .Include(p => p.Product)
                    .ThenInclude(p => p.Warehouses)
                 .Where(w => w.ProductId == productId)
                 .ToListAsync();


                if (result.Any())
                {
                    return result;
                }
                else
                {
                    throw new InvalidOperationException($"Warehouses not found for ProductId: {productId}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in BasketReporitory -> GetProductsAvailableStores: {ex.Message}");
            }
        }

        public async Task<UserCart> RemoveProductBasketAsync(DeleteBasketProductDto model)
        {
            try
            {
                var cartItem = await _storeLineContext.UserCarts
                    .Include(p => p.Product)
                    .Include(w => w.Store)
                    .FirstOrDefaultAsync(c => c.ProductId == model.ProductId && c.UserId == model.UserId);

                if (cartItem != null)
                {
                    if (cartItem.Quantity >= model.Quantity)
                    {
                        cartItem.Quantity -= model.Quantity;

                        if (cartItem.Quantity == 0)
                        {
                            _storeLineContext.UserCarts.Remove(cartItem);
                        }

                        await _storeLineContext.SaveChangesAsync();

                        return cartItem;
                    }
                    else
                    {
                        throw new Exception("Requested quantity to remove exceeds the quantity in the cart.");
                    }
                }
                else
                {
                    throw new Exception($"Product with ID {model.ProductId} not found in the user's cart.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in BasketReporitory -> RemoveProductBasketAsync: {ex.Message}");
            }
        }

        public async Task<List<UserCart>> RemoveAllUserBasketAsync(string userId)
        {
            try
            {
                var userExist = await _storeLineContext.Users.FirstOrDefaultAsync(i => i.Id == userId)
                   ?? throw new Exception($"User by id ({userId}) not found");

                var cartItems = await _storeLineContext.UserCarts
                    .Include(p => p.Product)
                    .Include(w => w.Store)
                    .Where(c => c.UserId == userId)
                    .ToListAsync();

                if (cartItems != null && cartItems.Any())
                {
                    _storeLineContext.UserCarts.RemoveRange(cartItems);

                    await _storeLineContext.SaveChangesAsync();
                    
                    return cartItems;
                }
                else
                {
                    throw new Exception($"User cart not found for UserId: ({userId})");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in BasketReporitory -> RemoveAllUserBasketAsync: {ex.Message}");
            }
        }

        public async Task<UserCart> UpdateBasketItemQuantityAsync(UpdateBasketItemDto model)
        {
            try
            {
                var cartItem = await _storeLineContext.UserCarts
                    .Include(s => s.Store)
                    .Include(p => p.Product)
                    .FirstOrDefaultAsync(c => c.UserId == model.UserId &&
                                              c.ProductId == model.ProductId &&
                                              c.StoreId == model.StoreId);

                if (cartItem != null)
                {
                    cartItem.Quantity = model.Quantity;

                    await _storeLineContext.SaveChangesAsync();

                    return cartItem;
                }
                else
                {
                    throw new Exception($"CartItem not found for " +
                        $"UserId: {model.UserId}, " +
                        $"ProductId: {model.ProductId}, " +
                        $"StoreId: {model.StoreId}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in BasketReporitory -> UpdateBasketItemQuantityAsync: {ex.Message}");
            }
        }

    }
}
