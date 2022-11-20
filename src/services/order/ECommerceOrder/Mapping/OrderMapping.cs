using AutoMapper;
using ECommerceCommon.EventBusModel;
using ECommerceOrder.Dtos;
using ECommerceOrder.Features.Command;
using ECommerceOrder.Models;

namespace ECommerceOrder.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<Order, OrderCreateEvent>().ReverseMap();
            CreateMap<CreateOrderRequest, Order>().ReverseMap();
            CreateMap<CreateOrderRequest, OrderCreateEvent>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}