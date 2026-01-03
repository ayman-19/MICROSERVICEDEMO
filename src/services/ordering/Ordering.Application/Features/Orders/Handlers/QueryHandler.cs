namespace Ordering.Application.Features.Orders.Handlers;

public sealed class OrderQueryHandler(IMapper mapper, IOrderRepository orderRepository)
    : IRequestHandler<PaginateOrdersRequest, PaginationResponse<IEnumerable<OrderDto>>>,
        IRequestHandler<GetOrderByIdRequest, ResponseOf<OrderDto>>
{
    public async Task<PaginationResponse<IEnumerable<OrderDto>>> Handle(
        PaginateOrdersRequest request,
        CancellationToken cancellationToken
    )
    {
        var totalCount = await orderRepository.CountAsync(
            o => EF.Functions.Like(o.UserName, $"%{request.Search}%"),
            cancellationToken
        );
        var orders = await orderRepository.PaginateAsync(
            request.PageIndex,
            request.PageSize,
            request.Search,
            cancellationToken
        );
        return new()
        {
            TotalCount = totalCount,
            Count = orders.Count(),
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Result = mapper.Map<IEnumerable<OrderDto>>(orders),
        };
    }

    public async Task<ResponseOf<OrderDto>> Handle(
        GetOrderByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        var order = await orderRepository.FindAsync(request.Id, cancellationToken);
        return new() { Result = mapper.Map<OrderDto>(order) };
    }
}
