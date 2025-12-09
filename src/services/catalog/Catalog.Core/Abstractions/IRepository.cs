namespace Catalog.Core.Abstractions;

public interface IRepository<TEntity>
    where TEntity : Entity
{
    ValueTask<bool> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    ValueTask<bool> Update(TEntity entity, CancellationToken cancellationToken = default);
    ValueTask<bool> Delete(TEntity entity, CancellationToken cancellationToken = default);
    ValueTask<TEntity> FindAsync(string id, CancellationToken cancellationToken = default);
    ValueTask<bool> AnyAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default
    );
    ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default);
    ValueTask<long> CountAsync(CancellationToken cancellationToken = default);
    ValueTask<long> CountAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default
    );
    ValueTask<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default
    );
}
