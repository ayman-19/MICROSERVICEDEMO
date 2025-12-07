namespace Catalog.Infrastructure.Implementations;

internal sealed class TypeRepository : Repository<ProductType>, ITypeRepository
{
    public TypeRepository(CatalogDbContext context)
        : base(context) { }
}
