using Application.DtoModels.Models.Admin;
using Application.DtoModels.Models.User.Category;
using Application.Services.Interfaces.IRepository.Admin;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistance.Context;

namespace Persistance.Repository.Admin
{
    public class DeliveryOptionRepository : IDeliveryOptionRepository
    {
        private readonly StoreLineContext _context;
        private readonly IMapper _mapper;

        public DeliveryOptionRepository(StoreLineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DeliveryOption> AddDeliveryOptionAsync(DeliveryOptionDTO deliveryDto)
        {
            try
            {
                var existingDelivery = await _context.DeliveryOptions
                    .FirstOrDefaultAsync(c => c.DeliveryType == deliveryDto.DeliveryType);

                if (existingDelivery != null)
                {
                    var store = await _context.Stores.FindAsync(deliveryDto.StoreId)
                        ?? throw new InvalidOperationException($"Store with ID {deliveryDto.StoreId} does not exist."); ;
                    
                    deliveryDto.StoreId = deliveryDto.StoreId;
                }

                var delivery = _mapper.Map<DeliveryOption>(deliveryDto);

                _context.DeliveryOptions.Add(delivery);

                await _context.SaveChangesAsync();

                return delivery;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in DeliveryOptionRepository -> AddDeliveryOptionAsync: {ex.Message}");
            }
        }

        public async Task<DeliveryOption> DeleteDeliveryOptionAsync(int deliveryId)
        {
            try
            {
                var delivery = await _context.DeliveryOptions.FindAsync(deliveryId)
                    ?? throw new KeyNotFoundException($"Delivery type with ID {deliveryId} not found");

                _context.DeliveryOptions.Remove(delivery);

                await _context.SaveChangesAsync();

                return delivery;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in DeliveryOptionRepository -> DeleteDeliveryOptionAsync: {ex.Message}");
            }
        }

        public async Task<DeliveryOption> UpdateDeliveryOptionAsync(DeliveryOptionDTO deliveryDto)
        {
            try
            {
                var deliveryOption = await _context.DeliveryOptions.FindAsync(deliveryDto.DeliveryId)
                    ?? throw new KeyNotFoundException($"The delivery option with ID {deliveryDto.DeliveryId} was not found.");

                if (!string.IsNullOrEmpty(deliveryDto.DeliveryType) && deliveryDto.DeliveryType != deliveryOption.DeliveryType)
                {
                    var existingDeliveryOption = await _context.DeliveryOptions
                        .FirstOrDefaultAsync(o => o.DeliveryType == deliveryDto.DeliveryType && o.DeliveryId != deliveryDto.DeliveryId);

                    if (existingDeliveryOption != null)
                    {
                        throw new InvalidOperationException($"Another delivery option with type '{deliveryDto.DeliveryType}' already exists.");
                    }
                }

                if (!string.IsNullOrEmpty(deliveryDto.DeliveryType))
                {
                    deliveryOption.DeliveryType = deliveryDto.DeliveryType;
                }

                if (deliveryDto.DeliveryTime > 0)
                {
                    deliveryOption.DeliveryTime = deliveryDto.DeliveryTime;
                }

                if (deliveryDto.DeliveryPrice > 0)
                {
                    deliveryOption.DeliveryPrice = deliveryDto.DeliveryPrice;
                }

                if (deliveryDto.StoreId > 0)
                {
                    var store = await _context.Stores.FindAsync(deliveryDto.StoreId);
                    if (store == null)
                    {
                        throw new InvalidOperationException($"Store with ID {deliveryDto.StoreId} does not exist.");
                    }

                    deliveryOption.StoreId = deliveryDto.StoreId;
                }

                await _context.SaveChangesAsync();

                return deliveryOption;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in DeliveryOptionRepository -> UpdateDeliveryOptionAsync: {ex.Message}");
            }
        }

    }
}
