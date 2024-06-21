using Application.DtoModels.Models.Admin;

namespace Application.Services.Interfaces.IServices.Admin
{
    public interface IWarehouseService
    {
        Task<List<WarehouseDTO>> GetAllWarehousesAsync();

        Task<WarehouseDTO> AddWarehouseAsync(WarehouseDTO warehouseDto);

        Task<WarehouseDTO> UpdateWarehouseAsync(WarehouseDTO warehouseDto);

        Task<WarehouseDTO> DeleteWarehouseAsync(int warehouseId);
    }
}
