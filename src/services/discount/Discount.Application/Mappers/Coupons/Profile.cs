namespace Discount.Application.Mappers.Coupons;

internal sealed class CouponProfile : Profile
{
    public CouponProfile()
    {
        CreateMap<Coupon, DiscountModel>().ReverseMap();
        CreateMap<CreateDiscountRequest, Coupon>();
        CreateMap<UpdateDiscountRequest, Coupon>();
    }
}
