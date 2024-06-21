using Application.DtoModels.Models.User.Order;
using Application.DtoModels.Response.User.Order;
using AutoMapper;
using Domain.Models;
using Persistance;

namespace Application.MappingProfile.User
{
    public class MppingOrder : Profile
    {
        public MppingOrder()
        {
            CreateMap<CreateOrder, Order>();
            CreateMap<Order, CreateOrder>();
            CreateMap<UserCart, OrderItem>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Price * src.Quantity))
                .ForMember(dest => dest.Store, opt => opt.MapFrom(src => src.Store.StoreName));
            CreateMap<Order, OrderResponseDto>()
                 .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
                 .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.OrderItems.FirstOrDefault().Product.ProductName))
                 .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.OrderItems.FirstOrDefault().ProductPrice))
                 .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.OrderItems.Sum(oi => oi.TotalPrice)))
                 .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.OrderItems.Sum(oi => oi.Quantity)))
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Cart.User.UserName))
                 .ForMember(dest => dest.UserAddress, opt => opt.MapFrom(src => src.Cart.User.Address))
                 .ForMember(dest => dest.UserPhone, opt => opt.MapFrom(src => src.Cart.User.PhoneNumber))
                 .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                 .ForMember(dest => dest.DeliveryType, opt => opt.MapFrom(src => src.Delivery.DeliveryType))
                 .ForMember(dest => dest.DeliveryPrice, opt => opt.MapFrom(src => src.Delivery.DeliveryPrice))
                 .ForMember(dest => dest.DeliveryTime, opt => opt.MapFrom(src => src.Delivery.DeliveryTime))
                 .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Cart.Store.StoreName))
                 .ForMember(dest => dest.StoreCity, opt => opt.MapFrom(src => src.Cart.Store.City))
                 .ForMember(dest => dest.StoreAddress, opt => opt.MapFrom(src => src.Cart.Store.Address))
                 .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.Payment.Type))
                 .ForMember(dest => dest.Commission, opt => opt.MapFrom(src => src.Payment.Commission))
                 .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.StatusName));

            CreateMap<DeliveryOption, DeliveryOptionDto>();
            CreateMap<OrderStatus, OrderStatusDto>();
            CreateMap<Payment, PaymentDto>();
            CreateMap<Order, UpdateOrderStatusDto>();
            CreateMap<UpdateOrderStatusDto, Order>();
        }
    }
}
