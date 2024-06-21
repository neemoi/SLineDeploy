using Application.DtoModels.Models.User.Cart;
using Application.DtoModels.Response.User.Basket;
using Application.DtoModels.Response.User.Product;
using AutoMapper;
using Persistance;

namespace Application.MappingProfile.User
{
    public class MappingBasket : Profile
    {
        public MappingBasket()
        {
            CreateMap<BasketDto, BasketResponseDto>();
            CreateMap<BasketDto, UserCart>();
            CreateMap<UserCart, BasketResponseDto>()
                .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.CartId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.StoreName))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Store.City))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Store.Address))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.IsOrdered, opt => opt.MapFrom(src => src.IsOrdered))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Product.Image));
            CreateMap<Warehouse, GetProductsStoresResponseDto>()
                .ForMember(dest => dest.StoreId, opt => opt.MapFrom(src => src.StoreId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.StoreName))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Store.City))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Store.Address))
               .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
               .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
               .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.ProductPrice))
               .ForMember(dest => dest.DeliveryTime, opt => opt.MapFrom(src => src.Store.DeliveryOptions.Any() ? src.Store.DeliveryOptions.First().DeliveryTime : 0))
               .ForMember(dest => dest.DeliveryPrice, opt => opt.MapFrom(src => src.Store.DeliveryOptions.Any() ? src.Store.DeliveryOptions.First().DeliveryPrice : 0))
               .ForMember(dest => dest.DeliveryType, opt => opt.MapFrom(src => src.Store.DeliveryOptions.Any() ? src.Store.DeliveryOptions.First().DeliveryType : null));
        }
    }
}
