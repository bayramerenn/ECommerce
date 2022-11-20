using AutoMapper;
using ECommerceBasket.Dtos;
using ECommerceCommon.EventBusModel;
using ECommerceBasket.Model;

namespace ECommerceBasket.Mapping
{
    public class BasketMapping : Profile
    {
        public BasketMapping()
        {
            CreateMap<Basket, OrderCreateEvent>().ReverseMap();
            CreateMap<BasketDto, Basket>().ReverseMap();
        }
    }
}