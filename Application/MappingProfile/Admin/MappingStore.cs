using Application.DtoModels.Models.Admin;
using AutoMapper;
using Persistance;

namespace Application.MappingProfile.Admin
{
    public class MappingStore : Profile
    {
        public MappingStore()
        {
            CreateMap<Store, StoreDTO>();
            CreateMap<StoreDTO, Store>();

            CreateMap<ChainOfStore, ChainOfStoreDTO>();
            CreateMap<ChainOfStoreDTO, ChainOfStore>();
        }
    }
}
