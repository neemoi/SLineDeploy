using Application.DtoModels.Models.Admin;
using Application.Services.Interfaces.IServices.Admin;
using Application.UnitOfWork;
using AutoMapper;

namespace Application.Services.Implementations.Admin
{
    public class DeliveryOptionService : IDeliveryOptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeliveryOptionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DeliveryOptionDTO> AddDeliveryOptionAsync(DeliveryOptionDTO deliveryDto)
        {
            try
            {
                var result = await _unitOfWork.DeliveryOptionRepository.AddDeliveryOptionAsync(deliveryDto);

                return _mapper.Map<DeliveryOptionDTO> (result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in DeliveryOptionService -> AddDeliveryOptionAsync: {ex.Message}");
            }
        }

        public async Task<DeliveryOptionDTO> DeleteDeliveryOptionAsync(int deliveryId)
        {
            try
            {
                var result = await _unitOfWork.DeliveryOptionRepository.DeleteDeliveryOptionAsync(deliveryId);

                return _mapper.Map<DeliveryOptionDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in DeliveryOptionService -> DeleteDeliveryOptionAsync: {ex.Message}");
            }
        }

        public async Task<DeliveryOptionDTO> UpdateDeliveryOptionAsync(DeliveryOptionDTO deliveryDto)
        {
            try
            {
                var result = await _unitOfWork.DeliveryOptionRepository.UpdateDeliveryOptionAsync(deliveryDto);

                return _mapper.Map<DeliveryOptionDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in DeliveryOptionService -> UpdateDeliveryOptionAsync: {ex.Message}");
            }
        }
    }
}