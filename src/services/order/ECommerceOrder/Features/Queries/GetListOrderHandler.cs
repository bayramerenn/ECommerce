using AutoMapper;
using ECommerceCommon.Contants;
using ECommerceCommon.Exceptions;
using ECommerceOrder.Dtos;
using ECommerceOrder.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerceOrder.Features.Queries
{
    public class GetListOrderHandler : IRequestHandler<GetListOrderRequest, GetListOrderResponse>
    {
        private readonly ECommerceContext _context;
        private readonly IMapper _mapper;

        public GetListOrderHandler(ECommerceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetListOrderResponse> Handle(GetListOrderRequest request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.AsNoTracking().ToListAsync();
            if (order == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, new { invoice = Messages.OrderNullError });

            var basketDto = _mapper.Map<List<OrderDto>>(order);

            return new GetListOrderResponse
            {
                Data = basketDto
            };
        }
    }
}