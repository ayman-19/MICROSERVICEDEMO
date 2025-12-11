namespace Catalog.Core.Entities.Products;

public interface IProductRepository : IRepository<Product>
{
    ValueTask<IReadOnlyList<Product>> PaginateAsync(
        int pageIndex,
        int pageSize,
        string? search,
        SortDirection sortDirection,
        CancellationToken cancellationToken = default
    );
}
