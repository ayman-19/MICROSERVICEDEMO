namespace Catalog.Core.Entities.Brands;

public interface IBrandRepository : IRepository<Brand>
{
    ValueTask<IReadOnlyList<Brand>> PaginateAsync(
        int pageIndex,
        int pageSize,
        string? search,
        SortDirection sortDirection,
        CancellationToken cancellationToken = default
    );
}
