using Application.DtoModels.Models.User.Profile;
using Application.DtoModels.Response.User.Profile;
using AutoMapper;
using Persistance;

namespace Application.MappingProfile.User
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Users, ProfileResposneDto>();
            CreateMap<EditProfileDto, Users>();
            CreateMap<string, Users>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src));
        }
    }
}
