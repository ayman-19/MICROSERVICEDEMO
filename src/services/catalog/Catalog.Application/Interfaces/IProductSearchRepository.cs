namespace Catalog.Application.Interfaces;

public interface IProductSearchRepository
{
    Task CreateAsync(ProductSearchDocument product, CancellationToken ct);
    Task UpdateAsync(ProductSearchDocument product, CancellationToken ct);
    Task DeleteAsync(string id, CancellationToken ct);
    Task<ProductSearchDocument> GetByIdAsync(string id, CancellationToken ct);
    Task<IEnumerable<ProductSearchDocument>> SearchAsync(
        string query,
        int page,
        int size,
        CancellationToken ct
    );
}
