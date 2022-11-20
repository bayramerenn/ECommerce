using AutoMapper;
using ECommerceBasket.Model;
using ECommerceBasket.Services;
using ECommerceCommon.Contants;
using ECommerceCommon.Exceptions;
using MediatR;

namespace ECommerceBasket.Features.Command
{
    public class CreateBasketHandler : IRequestHandler<CreateBasketRequest, CreateBasketResponse>
    {
        private readonly IBasketService _basketService;
        private IMapper _mapper;

        public CreateBasketHandler(IBasketService basketService, IMapper mapper)
        {
            _basketService = basketService;
            _mapper = mapper;
        }

        public async Task<CreateBasketResponse> Handle(CreateBasketRequest request, CancellationToken cancellationToken)
        {
            var baskets = _mapper.Map<Basket[]>(request.Data);

            var result = await _basketService.Save(baskets);

            if (!result)
                throw new RestException(System.Net.HttpStatusCode.BadRequest, new { CurrentAccount = Messages.BasketCreateError });

            return new CreateBasketResponse
            {
                Message = Messages.SuccessCreateBasket,
                Success = true
            };
        }
    }
}