using Application.DtoModels.Models.Admin;
using AutoMapper;
using Persistance;

namespace Application.MappingProfile.Admin
{
    public class MappingWarehouse : Profile
    {
        public MappingWarehouse()
        {
            CreateMap<Warehouse, WarehouseDTO>();
            CreateMap<WarehouseDTO, Warehouse>();
        }
    }
}
