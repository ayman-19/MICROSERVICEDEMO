namespace Catalog.Infrastructure.Implementations;

internal sealed class BrandRepository : Repository<Brand>, IBrandRepository
{
    public BrandRepository(CatalogDbContext context)
        : base(context) { }

    public async ValueTask<IReadOnlyList<Brand>> PaginateAsync(
        int pageIndex,
        int pageSize,
        string search,
        CancellationToken cancellationToken = default
    )
    {
        var query = _collection.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => p.Name.Contains(search));
        }

        return await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}
