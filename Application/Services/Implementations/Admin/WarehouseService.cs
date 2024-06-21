using Application.DtoModels.Models.Admin;
using Application.Services.Interfaces.IServices.Admin;
using Application.UnitOfWork;
using AutoMapper;

namespace Application.Services.Implementations.Admin
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WarehouseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<WarehouseDTO> AddWarehouseAsync(WarehouseDTO warehouseDto)
        {

            try
            {
                var result = await _unitOfWork.WarehouseRepository.AddWarehouseAsync(warehouseDto);

                return _mapper.Map<WarehouseDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in WarehouseService -> AddWarehouseAsync: {ex.Message}");
            }
        }

        public async Task<WarehouseDTO> DeleteWarehouseAsync(int warehouseId)
        {
            try
            {
                var result = await _unitOfWork.WarehouseRepository.DeleteWarehouseAsync(warehouseId);

                return _mapper.Map<WarehouseDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in WarehouseService -> DeleteWarehouseAsync: {ex.Message}");
            }
        }

        public async Task<List<WarehouseDTO>> GetAllWarehousesAsync()
        {
            try
            {
                var result = await _unitOfWork.WarehouseRepository.GetAllWarehousesAsync();

                return _mapper.Map<List<WarehouseDTO>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in WarehouseService -> GetWarehousesAsync: {ex.Message}");
            }
        }

        public async Task<WarehouseDTO> UpdateWarehouseAsync(WarehouseDTO warehouseDto)
        {
            try
            {
                var result = await _unitOfWork.WarehouseRepository.UpdateWarehouseAsync(warehouseDto);

                return _mapper.Map<WarehouseDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in WarehouseService -> UpdateWarehouseAsync: {ex.Message}");
            }
        }
    }
}
