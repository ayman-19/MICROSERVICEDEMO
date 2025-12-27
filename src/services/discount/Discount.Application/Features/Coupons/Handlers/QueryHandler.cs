namespace Discount.Application.Features.Coupons.Handlers;

internal sealed record DiscountQueryHandler(ICouponRepository CouponRepository, IMapper Mapper)
    : IRequestHandler<GetDiscountByIdQuery, DiscountModel>,
        IRequestHandler<GetDiscountByNameQuery, DiscountModel>,
        IRequestHandler<PaginateDiscountQuery, PaginationResponse<IEnumerable<DiscountModel>>>
{
    public async Task<DiscountModel> Handle(
        GetDiscountByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var exist = await CouponRepository.AnyAsync(request.Id, cancellationToken);
        if (!exist)
            return new();
        var coupon = await CouponRepository.GetAsync(request.Id, cancellationToken);
        return new DiscountModel
        {
            Id = (int)coupon.Id,
            ProductName = coupon.ProductName,
            Description = coupon.Description,
            Amount = (double)coupon.Amount,
        };
        //return Mapper.Map<DiscountModel>(coupon);
    }

    public async Task<PaginationResponse<IEnumerable<DiscountModel>>> Handle(
        PaginateDiscountQuery request,
        CancellationToken cancellationToken
    )
    {
        var pagedCoupons = await CouponRepository.PaginateAsync(
            request.PageIndex,
            request.PageSize,
            request.Search,
            cancellationToken
        );

        var totalCount = await CouponRepository.TotalCountAsync(cancellationToken);
        return new()
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            Count = pagedCoupons.Count(),
            Result = Mapper.Map<IEnumerable<DiscountModel>>(pagedCoupons),
        };
    }

    public async Task<DiscountModel> Handle(
        GetDiscountByNameQuery request,
        CancellationToken cancellationToken
    )
    {
        var exist = await CouponRepository.AnyAsync(request.name, cancellationToken);
        if (!exist)
            return new();
        var coupon = await CouponRepository.GetAsync(request.name, cancellationToken);
        return Mapper.Map<DiscountModel>(coupon);
    }
}
