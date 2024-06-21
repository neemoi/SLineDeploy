using Application.DtoModels.Response.User.Store;
using AutoMapper;
using Persistance;

namespace Application.MappingProfile.User
{
    public class MappingInformationAboutStoresStore : Profile
    {
        public MappingInformationAboutStoresStore()
        {
            CreateMap<Store, StoreResponseDto>();
            CreateMap<ChainOfStore, ChainOfStoreResponseDto>();
        }
    }
}
