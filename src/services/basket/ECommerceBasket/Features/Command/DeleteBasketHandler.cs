using ECommerceCommon.Contants;
using ECommerceCommon.Exceptions;
using ECommerceBasket.Services;
using MediatR;

namespace ECommerceBasket.Features.Command
{
    public class DeleteBasketHandler : IRequestHandler<DeleteBasketRequest, DeleteBasketResponse>
    {
        private readonly IBasketService _basketService;

        public DeleteBasketHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<DeleteBasketResponse> Handle(DeleteBasketRequest request, CancellationToken cancellationToken)
        {
            var result = await _basketService.Delete(request.BasketId);
            if (!result)
                throw new RestException(System.Net.HttpStatusCode.BadRequest, new { CurrentAccount = Messages.BasketDeleteError });
            return new DeleteBasketResponse
            {
                Message = Messages.SuccessDeleteBasket,
                Success = true
            };
        }
    }
}