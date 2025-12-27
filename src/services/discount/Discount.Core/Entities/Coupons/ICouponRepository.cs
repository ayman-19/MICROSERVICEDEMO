namespace Discount.Core.Entities.Coupons;

public interface ICouponRepository
{
    ValueTask<Coupon> CreateAsync(Coupon coupon, CancellationToken cancellationToken = default);
    ValueTask<Coupon> UpdateAsync(Coupon coupon, CancellationToken cancellationToken = default);
    ValueTask<Coupon> DeleteAsync(Coupon coupon, CancellationToken cancellationToken = default);
    ValueTask<Coupon> GetAsync(long id, CancellationToken cancellationToken = default);
    ValueTask<Coupon> GetAsync(string name, CancellationToken cancellationToken = default);
    ValueTask<bool> AnyAsync(long id, CancellationToken cancellationToken = default);
    ValueTask<bool> AnyAsync(string name, CancellationToken cancellationToken = default);

    ValueTask<int> TotalCountAsync(CancellationToken cancellationToken = default);

    ValueTask<IEnumerable<Coupon>> PaginateAsync(
        int pageIndex,
        int pageSize,
        string search,
        CancellationToken cancellationToken = default
    );
}
