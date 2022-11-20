using AutoMapper;
using ECommerceBasket.Dtos;
using ECommerceBasket.Model;
using ECommerceCommon.EventBusModel;

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