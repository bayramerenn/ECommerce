using AutoMapper;
using ECommerceBasket.Services;
using ECommerceCommon.Contants;
using ECommerceCommon.EventBusModel;
using ECommerceCommon.Exceptions;
using MassTransit;
using MediatR;

namespace ECommerceBasket.Features.Command
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderRequest, CreateOrderResponse>
    {
        private readonly IBasketService _basketService;
        private IMapper _mapper;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public CreateOrderHandler(IBasketService basketService, IMapper mapper, ISendEndpointProvider sendEndpointProvider)
        {
            _basketService = basketService;
            _mapper = mapper;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task<CreateOrderResponse> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var basket = await _basketService.Get(request.BasketId);

            if (basket == null || basket.Length == 0)
                throw new RestException(System.Net.HttpStatusCode.NotFound, new { invoice = Messages.NotFoundBasketError(request.BasketId) });

            var orderCreateEvent = _mapper.Map<OrderCreateEvent[]>(basket);
            try
            {
                var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(EventBusConstans.QueueCreateOrderSend));
                var resource = new OrderCreateEventResource
                {
                    BasketId = request.BasketId,
                    OrderCreateEvents = orderCreateEvent,
                };

                await sendEndpoint.Send<OrderCreateEventResource>(resource);
            }
            catch (Exception)
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest, new { CurrentAccount = Messages.EventOrderCreateError });
            }
            return new CreateOrderResponse
            {
                Message = Messages.SuccessOrderCreate,
                Success = true
            };
        }
    }
}