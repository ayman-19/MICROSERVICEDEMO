namespace Basket.Infrastructure.Implementaions;

internal sealed class ShopingCartItemRepository
    : Repository<ShopingCartItem>,
        IShopingCartItemRepository
{
    public ShopingCartItemRepository(IConnectionMultiplexer multiplexer, IDistributedCache cache)
        : base(multiplexer, cache) { }
}
