using Application.DtoModels.Models.Admin;
using AutoMapper;
using Persistance;

namespace Application.MappingProfile.Admin
{
    public class MappingSubCategory : Profile
    {
        public MappingSubCategory()
        {
            CreateMap<Subcategory, SubCategoryDTO>();
            CreateMap<SubCategoryDTO, Subcategory>();
        }
    }
}
