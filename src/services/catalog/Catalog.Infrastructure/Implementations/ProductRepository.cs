namespace Catalog.Infrastructure.Implementations;

internal sealed class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(CatalogDbContext context)
        : base(context) { }

    public async ValueTask<IReadOnlyList<Product>> PaginateAsync(
        int pageIndex,
        int pageSize,
        string? search,
        CancellationToken cancellationToken = default
    )
    {
        var query = _collection.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p =>
                p.Name.Contains(search)
                || p.Description.Contains(search)
                || p.Summary.Contains(search)
            );
        }

        return await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}
