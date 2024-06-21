using Application.DtoModels.Models.Admin;
using AutoMapper;
using Persistance;
public class MappingDeliveryOptions : Profile
{
    public MappingDeliveryOptions()
    {
        CreateMap<DeliveryOption, DeliveryOptionDTO>();
        CreateMap<DeliveryOptionDTO, DeliveryOption>();
    }
}
