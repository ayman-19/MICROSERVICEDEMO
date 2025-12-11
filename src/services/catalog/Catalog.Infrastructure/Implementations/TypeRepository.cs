namespace Catalog.Infrastructure.Implementations;

internal sealed class TypeRepository : Repository<ProductType>, ITypeRepository
{
    public TypeRepository(CatalogDbContext context)
        : base(context) { }

    public async ValueTask<IReadOnlyList<ProductType>> PaginateAsync(
        int pageIndex,
        int pageSize,
        string search,
        SortDirection sortDirection,
        CancellationToken cancellationToken = default
    )
    {
        var query = _collection.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => p.Name.Contains(search));
        }

        query = sortDirection switch
        {
            SortDirection.Ascending => query.OrderBy(p => p.Name),
            SortDirection.Descending => query.OrderByDescending(p => p.Name),
            _ => query,
        };

        return await query.Paginate(pageIndex, pageSize).ToListAsync(cancellationToken);
    }
}
