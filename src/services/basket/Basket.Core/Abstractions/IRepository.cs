namespace Basket.Core.Abstractions;

public interface IRepository<TEntity>
    where TEntity : Entity
{
    ValueTask<bool> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    ValueTask<bool> Update(TEntity entity, CancellationToken cancellationToken = default);
    ValueTask<bool> Delete(TEntity entity, CancellationToken cancellationToken = default);
    ValueTask<TEntity> FindAsync(Guid id, CancellationToken cancellationToken = default);
    ValueTask<bool> AnyAsync(Guid id, CancellationToken cancellationToken = default);
    ValueTask<int> CountAsync(CancellationToken cancellationToken = default);
    ValueTask<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
}
