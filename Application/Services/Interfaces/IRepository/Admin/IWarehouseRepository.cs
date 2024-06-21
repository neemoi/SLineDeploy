using Application.DtoModels.Models.Admin;
using Persistance;

namespace Application.Services.Interfaces.IRepository.Admin
{
    public interface IWarehouseRepository
    {
        Task<List<Warehouse>> GetAllWarehousesAsync();

        Task<Warehouse> AddWarehouseAsync(WarehouseDTO warehouseDto);

        Task<Warehouse> UpdateWarehouseAsync(WarehouseDTO warehouseDto);

        Task<Warehouse> DeleteWarehouseAsync(int warehouseId);
    }
}
