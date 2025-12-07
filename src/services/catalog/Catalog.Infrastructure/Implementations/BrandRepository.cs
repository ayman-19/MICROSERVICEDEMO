namespace Catalog.Infrastructure.Implementations;

internal sealed class BrandRepository : Repository<Brand>, IBrandRepository
{
    public BrandRepository(CatalogDbContext context)
        : base(context) { }
}
