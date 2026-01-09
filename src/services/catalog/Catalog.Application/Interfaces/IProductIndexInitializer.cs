namespace Catalog.Application.Interfaces;

public interface IProductIndexInitializer
{
    Task CreateIndexAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(CancellationToken cancellationToken = default);
}
