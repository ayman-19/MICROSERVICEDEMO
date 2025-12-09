namespace Catalog.Core.Entities.Types;

public interface ITypeRepository : IRepository<ProductType>
{
    ValueTask<IReadOnlyList<ProductType>> PaginateAsync(
        int pageIndex,
        int pageSize,
        string search,
        CancellationToken cancellationToken = default
    );
}
