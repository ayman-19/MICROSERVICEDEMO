namespace Discount.Application.Features.Coupons.Handlers;

public sealed record DiscountCommandHandler(ICouponRepository couponRepository, IMapper mapper)
    : IRequestHandler<CreateDiscountCommand, DiscountModel>,
        IRequestHandler<UpdateDiscountCommand, DiscountModel>,
        IRequestHandler<DeleteDiscountCommand, DiscountModel>
{
    public async Task<DiscountModel> Handle(
        CreateDiscountCommand request,
        CancellationToken cancellationToken
    )
    {
        var coupon = mapper.Map<Coupon>(request.request);
        var couponCreated = await couponRepository.CreateAsync(coupon, cancellationToken);
        return mapper.Map<DiscountModel>(couponCreated);
    }

    public async Task<DiscountModel> Handle(
        UpdateDiscountCommand request,
        CancellationToken cancellationToken
    )
    {
        var exist = await couponRepository.AnyAsync(request.request.Id, cancellationToken);
        if (!exist)
            return new();

        var coupon = await couponRepository.GetAsync(request.request.Id, cancellationToken);

        mapper.Map(request.request, coupon);

        var couponUpdated = await couponRepository.UpdateAsync(coupon, cancellationToken);

        return mapper.Map<DiscountModel>(couponUpdated);
    }

    public async Task<DiscountModel> Handle(
        DeleteDiscountCommand request,
        CancellationToken cancellationToken
    )
    {
        var exist = await couponRepository.AnyAsync(request.request.Id, cancellationToken);
        if (!exist)
            return new();

        var coupon = await couponRepository.GetAsync(request.request.Id, cancellationToken);
        var couponDeleted = await couponRepository.DeleteAsync(coupon, cancellationToken);
        return mapper.Map<DiscountModel>(couponDeleted);
    }
}
