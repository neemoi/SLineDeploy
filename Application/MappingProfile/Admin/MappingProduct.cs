using Application.DtoModels.Models.Admin;
using AutoMapper;
using Persistance;

namespace Application.MappingProfile.Admin
{
    public class MappingProduct : Profile
    {
        public MappingProduct()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
        }
    }
}