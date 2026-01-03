namespace Ordering.Application.Features.Orders.Requests;

public sealed record PaginateOrdersRequest
    : PaginateRequest,
        IRequest<PaginationResponse<IEnumerable<OrderDto>>>;

public sealed record GetOrderByIdRequest(long Id) : IRequest<ResponseOf<OrderDto>>;
