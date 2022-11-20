using ECommerceBasket.Dtos;
using ECommerceBasket.Dtos;
using ECommerceBasket.Model;

namespace ECommerceBasket.Services
{
    public interface IBasketService
    {
        Task<Basket[]> Get(string id);
        Task<bool> Save(Basket[] basket);
        Task<bool> Delete(string id);
    }
}
