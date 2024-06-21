using Application.DtoModels.Models.Authorization;
using Application.DtoModels.Response.Authorization;
using AutoMapper;
using Persistance;

namespace Application.MappingProfile.User
{
    public class MappingAuthorization : Profile
    {
        public MappingAuthorization()
        {
            CreateMap<RegisterDto, Users>();
            CreateMap<Users, LoginResponseDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address)); 
            CreateMap<Users, RegisterResponseDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Token, opt => opt.MapFrom((src, dest, token) => token));
            CreateMap<Users, LogoutResponseDto>();
        }
    }
}
