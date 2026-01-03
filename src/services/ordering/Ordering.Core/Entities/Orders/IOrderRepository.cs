namespace Ordering.Core.Entities.Orders;

public interface IOrderRepository : IRepository<Order>
{
    ValueTask<IEnumerable<Order>> PaginateAsync(
        int pageIndex,
        int pageSize,
        string search,
        CancellationToken cancellationToken = default
    );
}
