namespace Basket.Infrastructure.Implementaions;

internal sealed class CheckOutRepository : Repository<CheckOut>, ICheckOutRepository
{
    public CheckOutRepository(IConnectionMultiplexer multiplexer, IDistributedCache cache)
        : base(multiplexer, cache) { }
}
