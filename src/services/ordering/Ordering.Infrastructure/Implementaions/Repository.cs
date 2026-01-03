namespace Ordering.Infrastructure.Implementaions;

internal class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{
    protected readonly OrderDbContext _context;
    protected readonly DbSet<TEntity> _entities;

    public Repository(OrderDbContext context)
    {
        _context = context;
        _entities = _context.Set<TEntity>();
    }

    public bool Any(Expression<Func<TEntity, bool>> pridecate) => _entities.Any(pridecate);

    public bool Any() => _entities.Any();

    public async ValueTask<bool> AnyAsync(
        Expression<Func<TEntity, bool>> pridecate,
        CancellationToken cancellationToken = default
    ) => await _entities.AnyAsync(pridecate, cancellationToken);

    public async ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default) =>
        await _entities.AnyAsync(cancellationToken);

    public async Task<int> CountAsync(CancellationToken cancellationToken = default) =>
        await _entities.CountAsync(cancellationToken);

    public async Task<int> CountAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default
    ) => await _entities.CountAsync(filter, cancellationToken);

    public EntityEntry<TEntity> Create(TEntity entity) => _entities.Add(entity);

    public async ValueTask<EntityEntry<TEntity>> CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    ) => await _entities.AddAsync(entity, cancellationToken);

    public void CreateRange(IEnumerable<TEntity> entities) => _entities.AddRange(entities);

    public async Task CreateRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default
    ) => await _entities.AddRangeAsync(entities, cancellationToken);

    public EntityEntry<TEntity> Delete(TEntity entity) => _entities.Remove(entity);

    public void DeleteRange(IEnumerable<TEntity> entities) => _entities.RemoveRange(entities);

    public async Task<int> ExecuteDeleteAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default
    ) => await _entities.Where(filter).ExecuteDeleteAsync(cancellationToken);

    public TEntity Find(long id, CancellationToken cancellationToken = default) =>
        _entities.Find(id)!;

    public async ValueTask<TEntity> FindAsync(
        long id,
        CancellationToken cancellationToken = default
    ) => await _entities.FindAsync(id, cancellationToken);

    public async Task<TEntity> FindAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default
    ) => await _entities.FirstOrDefaultAsync(filter, cancellationToken)!;

    public EntityEntry<TEntity> Update(TEntity entity) => _entities.Update(entity);

    public void UpdateRange(IEnumerable<TEntity> entities) => _entities.UpdateRange(entities);
}
