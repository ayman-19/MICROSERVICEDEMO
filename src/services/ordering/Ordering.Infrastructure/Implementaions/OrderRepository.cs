namespace Ordering.Infrastructure.Implementaions;

internal sealed class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(OrderDbContext context)
        : base(context) { }

    public async ValueTask<IEnumerable<Order>> PaginateAsync(
        int pageIndex,
        int pageSize,
        string search,
        CancellationToken cancellationToken = default
    )
    {
        var query = _entities.AsNoTracking().OrderByDescending(o => o.CreatedOn).AsQueryable();
        if (search.HasValue())
            query = query.Where(o => EF.Functions.Like(o.UserName, $"%{search}%"));
        query = query.Paginate(pageIndex, pageSize);
        return await query.ToListAsync(cancellationToken);
    }
}
