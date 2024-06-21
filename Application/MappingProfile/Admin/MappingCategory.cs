using Application.DtoModels.Models.Admin;
using AutoMapper;
using Persistance;

namespace CategoryAPI.Mapping;

public class MappingCategory : Profile
{
    public MappingCategory()
    {
        CreateMap<Category, CategoryDTO>();
        CreateMap<CategoryDTO, Category>();
    }
}
