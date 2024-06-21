using Application.DtoModels.Models.Admin;
using Application.Services.Interfaces.IRepository.Admin;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;

namespace Persistance.Repository.Admin
{
    public class StoreRepository : IStoreRepository
    {
        private readonly StoreLineContext _context;
        private readonly IMapper _mapper;

        public StoreRepository(StoreLineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Store> AddStoreAsync(StoreDTO storeDto)
        {
            try
            {
                var existingStore = await _context.Stores
               .FirstOrDefaultAsync(c => c.StoreName == storeDto.StoreName);

                if (existingStore != null)
                {
                    throw new InvalidOperationException($"A store named {storeDto.StoreName} already exists.");
                }

                if (storeDto.ChainId.HasValue)
                {
                    var existingChain = await _context.ChainOfStores.FindAsync(storeDto.ChainId.Value)
                        ?? throw new InvalidOperationException($"Chain with ID {storeDto.ChainId} does not exist.");
                }

                var store = _mapper.Map<Store>(storeDto);

                _context.Stores.Add(store);

                await _context.SaveChangesAsync();

                return store;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in StoreRepository -> AddStoreAsync: {ex.Message}");
            }
        }

        public async Task<Store> DeleteStoreAsync(int storeId)
        {
            try
            {
                var store = await _context.Stores.FindAsync(storeId)
                    ?? throw new KeyNotFoundException($"Store with ID {storeId} not found");

                _context.Stores.Remove(store);

                await _context.SaveChangesAsync();

                return store;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in StoreRepository -> DeleteStoreAsync: {ex.Message}");
            }
        }

        public async Task<List<Store>> GetAllStoreAsync()
        {
            try
            {
                var stores = await _context.Stores.ToListAsync();

                return _mapper.Map<List<Store>>(stores);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in StoreRepository -> GetAllStoreAsync: {ex.Message}");
            }
        }

        public async Task<Store> UpdateStoreAsync(StoreDTO storeDto)
        {
            try
            {
                var existingStore = await _context.Stores.FindAsync(storeDto.StoreId)
                    ?? throw new KeyNotFoundException($"Store with ID {storeDto.StoreId} does not exist.");

                if (!string.IsNullOrEmpty(storeDto.StoreName))
                {
                    existingStore.StoreName = storeDto.StoreName;
                }

                if (!string.IsNullOrEmpty(storeDto.City))
                {
                    existingStore.City = storeDto.City;
                }

                if (!string.IsNullOrEmpty(storeDto.Address))
                {
                    existingStore.Address = storeDto.Address;
                }

                if (storeDto.ChainId.HasValue)
                {
                    var existingChain = await _context.ChainOfStores.FindAsync(storeDto.ChainId.Value)
                        ?? throw new InvalidOperationException($"Chain with ID {storeDto.ChainId} does not exist.");

                    existingStore.ChainId = storeDto.ChainId.Value;
                }

                await _context.SaveChangesAsync();

                return existingStore;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in StoreRepository -> UpdateStoreAsync: {ex.Message}", ex);
            }
        }
    }
}