using Application.DtoModels.Models.Admin;

namespace Application.Services.Interfaces.IServices.Admin
{
    public interface IDeliveryOptionService
    {
        Task<DeliveryOptionDTO> AddDeliveryOptionAsync(DeliveryOptionDTO deliveryDto);

        Task<DeliveryOptionDTO> UpdateDeliveryOptionAsync(DeliveryOptionDTO deliveryDto);

        Task<DeliveryOptionDTO> DeleteDeliveryOptionAsync(int deliveryId);
    }
}
