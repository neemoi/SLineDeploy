using Application.DtoModels.Models.Admin;
using Application.Services.Interfaces.IRepository.Admin;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;

namespace Persistance.Repository.Admin
{
    public class ChainOfStoreRepository : IChainOfStoreRepository
    {
        private readonly StoreLineContext _context;
        private readonly IMapper _mapper;

        public ChainOfStoreRepository(StoreLineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ChainOfStore> AddChainOfStoreAsync(ChainOfStoreDTO chainStoreDto)
        {
            try
            {
                var existingChainStore = await _context.ChainOfStores
                    .FirstOrDefaultAsync(c => c.ChainName == chainStoreDto.ChainName);

                if (existingChainStore != null)
                {
                    throw new InvalidOperationException($"A store chain named {chainStoreDto.ChainName} already exists.");
                }
                
                var chainOfStore = _mapper.Map<ChainOfStore>(chainStoreDto);

                _context.ChainOfStores.Add(chainOfStore);

                await _context.SaveChangesAsync();

                return chainOfStore;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ChainOfStoreService -> AddChainOfStorAsync: {ex.Message}", ex);
            }
        }


        public async Task<ChainOfStore> DeleteChainOfStoreAsync(int chainStoreId)
        {
            try
            {
                var storeChain = await _context.ChainOfStores.FindAsync(chainStoreId)
                    ?? throw new KeyNotFoundException($"Store chain with ID {chainStoreId} not found");

                _context.ChainOfStores.Remove(storeChain);

                await _context.SaveChangesAsync();

                return storeChain;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ChainOfStoreService -> DeleteChainOfStorAsync: {ex.Message}");
            }
        }

        public async Task<List<ChainOfStore>> GetAllChainOfStoreAsync()
        {
            try
            {
                var stores = await _context.ChainOfStores.ToListAsync();

                return _mapper.Map<List<ChainOfStore>>(stores);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ChainOfStoreService -> GetAllChainOfStoreAsync: {ex.Message}");
            }
        }

        public async Task<ChainOfStore> UpdateChainOfStoreAsync(ChainOfStoreDTO chainStoreDto)
        {
            try
            {
                var existingStoreChain = await _context.ChainOfStores.FindAsync(chainStoreDto.ChainId)
                    ?? throw new KeyNotFoundException($"Store chain with ID {chainStoreDto.ChainId} does not exist.");

                if (!string.IsNullOrEmpty(chainStoreDto.ChainName))
                {
                    existingStoreChain.ChainName = chainStoreDto.ChainName;
                }

                if (!string.IsNullOrEmpty(chainStoreDto.Description))
                {
                    existingStoreChain.Description = chainStoreDto.Description;
                }

                await _context.SaveChangesAsync();

                return existingStoreChain;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ChainOfStoreService -> UpdateChainOfStorAsync: {ex.Message}", ex);
            }
        }
    }
}
