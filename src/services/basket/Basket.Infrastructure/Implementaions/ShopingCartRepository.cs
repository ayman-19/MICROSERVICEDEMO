namespace Basket.Infrastructure.Implementaions;

internal sealed class ShopingCartRepository : Repository<ShopingCart>, IShopingCartRepository
{
    public ShopingCartRepository(IConnectionMultiplexer multiplexer, IDistributedCache cache)
        : base(multiplexer, cache) { }
}
