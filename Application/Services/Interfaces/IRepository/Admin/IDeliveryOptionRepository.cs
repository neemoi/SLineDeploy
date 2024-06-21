using Application.DtoModels.Models.Admin;
using Persistance;

namespace Application.Services.Interfaces.IRepository.Admin
{
    public interface IDeliveryOptionRepository
    {
        Task<DeliveryOption> AddDeliveryOptionAsync(DeliveryOptionDTO deliveryDto);

        Task<DeliveryOption> UpdateDeliveryOptionAsync(DeliveryOptionDTO deliveryDto);

        Task<DeliveryOption> DeleteDeliveryOptionAsync(int deliveryId);
    }
}
