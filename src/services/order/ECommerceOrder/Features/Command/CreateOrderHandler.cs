using AutoMapper;
using ECommerceCommon.Contants;
using ECommerceOrder.Models;
using ECommerceOrder.Persistence;
using MediatR;

namespace ECommerceOrder.Features.Command
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderRequest, CreateOrderResponse>
    {
        private readonly ECommerceContext _context;
        private readonly IMapper _mapper;

        public CreateOrderHandler(ECommerceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateOrderResponse> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order[]>(request.Data);
            await _context.AddRangeAsync(order);
            await _context.SaveChangesAsync();

            return new CreateOrderResponse
            {
                Message = Messages.SuccessOrderCreate,
                Success = true
            };
        }
    }
}