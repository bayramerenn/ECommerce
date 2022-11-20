using AutoMapper;
using ECommerceBasket.Dtos;
using ECommerceBasket.Services;
using ECommerceCommon.Contants;
using ECommerceCommon.Exceptions;
using MediatR;

namespace ECommerceBasket.Features.Queries
{
    public class GetByIdOrderHandler : IRequestHandler<GetByIdOrderRequest, GetByIdOrderResponse>
    {
        private readonly IBasketService _basketService;
        private IMapper _mapper;

        public GetByIdOrderHandler(IBasketService basketService, IMapper mapper)
        {
            _basketService = basketService;
            _mapper = mapper;
        }

        public async Task<GetByIdOrderResponse> Handle(GetByIdOrderRequest request, CancellationToken cancellationToken)
        {
            var basket = await _basketService.Get(request.Id);
            if (basket is null)
            {
                throw new RestException(System.Net.HttpStatusCode.NotFound, new { invoice = Messages.NotFoundBasketError(request.Id) });
            }
            var basketDto = _mapper.Map<BasketDto[]>(basket);

            return new GetByIdOrderResponse
            {
                Basket = basketDto,
            };
        }
    }
}