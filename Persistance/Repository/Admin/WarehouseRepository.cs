using Application.DtoModels.Models.Admin;
using Application.Services.Interfaces.IRepository.Admin;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;

namespace Persistance.Repository.Admin
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly StoreLineContext _context;
        private readonly IMapper _mapper;

        public WarehouseRepository(StoreLineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Warehouse> AddWarehouseAsync(WarehouseDTO warehouseDto)
        {
            try
            {
                var existingWarehouse = await _context.Warehouses
                    .FirstOrDefaultAsync(c => c.WarehouseId == warehouseDto.WarehouseId);

                if (existingWarehouse != null)
                {
                    throw new InvalidOperationException($"A warehouse with ID {warehouseDto.WarehouseId} already exists.");
                }

                if (!warehouseDto.StoreId.HasValue)
                {
                    throw new InvalidOperationException("StoreId is required.");
                }

                var existingStore = await _context.Stores.FindAsync(warehouseDto.StoreId.Value)
                    ?? throw new InvalidOperationException($"Store with ID {warehouseDto.StoreId} does not exist.");


                if (!warehouseDto.ProductId.HasValue)
                {
                    throw new InvalidOperationException("ProductId is required.");
                }

                var existingProduct = await _context.Products.FindAsync(warehouseDto.ProductId.Value)
                    ?? throw new InvalidOperationException($"Product with ID {warehouseDto.ProductId} does not exist.");

                var warehouse = _mapper.Map<Warehouse>(warehouseDto);

                _context.Warehouses.Add(warehouse);

                await _context.SaveChangesAsync();

                return warehouse;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in WarehouseRepository -> AddWarehouseAsync: {ex.Message}", ex);
            }
        }

        public async Task<Warehouse> DeleteWarehouseAsync(int warehouseId)
        {
            try
            {
                var warehouse = await _context.Warehouses.FindAsync(warehouseId)
                    ?? throw new KeyNotFoundException($"Warehouse with ID {warehouseId} not found");

                _context.Warehouses.Remove(warehouse);

                await _context.SaveChangesAsync();

                return warehouse;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in WarehouseRepository -> DeleteWarehouseAsync: {ex.Message}");
            }
        }

        public async Task<List<Warehouse>> GetAllWarehousesAsync()
        {
            try
            {
                var warehouses = await _context.Warehouses.ToListAsync();

                return _mapper.Map<List<Warehouse>>(warehouses);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in WarehouseRepository -> GetWarehousesAsync: {ex.Message}");
            }
        }

        public async Task<Warehouse> UpdateWarehouseAsync(WarehouseDTO warehouseDto)
        {
            try
            {
                var warehouse = await _context.Warehouses.FindAsync(warehouseDto.WarehouseId)
                    ?? throw new KeyNotFoundException($"The warehouse with ID {warehouseDto.WarehouseId} was not found.");

                if (warehouseDto.StoreId.HasValue)
                {
                    var store = await _context.Stores.FindAsync(warehouseDto.StoreId.Value);
                    
                    if (store == null)
                    {
                        throw new InvalidOperationException($"Store with ID {warehouseDto.StoreId} does not exist.");
                    }

                    warehouse.StoreId = warehouseDto.StoreId.Value;
                }

                if (warehouseDto.ProductId.HasValue)
                {
                    var product = await _context.Products.FindAsync(warehouseDto.ProductId.Value);
                    
                    if (product == null)
                    {
                        throw new InvalidOperationException($"Product with ID {warehouseDto.ProductId} does not exist.");
                    }

                    warehouse.ProductId = warehouseDto.ProductId.Value;
                }

                if (warehouseDto.Quantity.HasValue)
                {
                    warehouse.Quantity = warehouseDto.Quantity.Value;
                }

                if (warehouseDto.ProductPrice.HasValue)
                {
                    warehouse.ProductPrice = warehouseDto.ProductPrice.Value;
                }

                if (warehouseDto.StoreId.HasValue)
                {
                    warehouse.ProductPrice = warehouseDto.ProductPrice.Value;
                }

                if (warehouseDto.ProductId.HasValue)
                {
                    warehouse.ProductPrice = warehouseDto.ProductId.Value;
                }

                await _context.SaveChangesAsync();

                return warehouse;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in WarehouseRepository -> UpdateWarehouseAsync: {ex.Message}", ex);
            }
        }
    }
}
