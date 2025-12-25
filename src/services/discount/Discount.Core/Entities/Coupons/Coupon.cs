namespace Discount.Core.Entities.Coupons;

public sealed record Coupon : Entity
{
    public string ProductName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
