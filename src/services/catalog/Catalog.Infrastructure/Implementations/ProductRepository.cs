namespace Catalog.Infrastructure.Implementations;

internal sealed class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(CatalogDbContext context)
        : base(context) { }
}
