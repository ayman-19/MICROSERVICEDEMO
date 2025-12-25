namespace Discount.Application.Features.Coupons.Requests;

public sealed record CreateDiscountCommand(CreateDiscountRequest request) : IRequest<DiscountModel>;

public sealed record UpdateDiscountCommand(UpdateDiscountRequest request) : IRequest<DiscountModel>;

public sealed record DeleteDiscountCommand(DeleteDiscountRequest request) : IRequest<DiscountModel>;
