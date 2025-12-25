namespace Discount.Application.Features.Coupons.Requests;

public sealed record GetDiscountByIdQuery(long Id) : IRequest<DiscountModel>;

public sealed record PaginateDiscountQuery
    : PaginateRequest,
        IRequest<PaginationResponse<IEnumerable<DiscountModel>>>;
