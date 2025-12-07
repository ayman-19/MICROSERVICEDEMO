namespace Catalog.Infrastructure.Implementations;

internal class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{
    private readonly IMongoCollection<TEntity> _collection;

    public Repository(CatalogDbContext context)
    {
        _collection = context.Database.GetCollection<TEntity>($"{typeof(TEntity).Name}s");
    }

    public async ValueTask<bool> AnyAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default
    ) => await _collection.Find(filter).AnyAsync(cancellationToken);

    public async ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default) =>
        await _collection.Find(_ => true).AnyAsync(cancellationToken);

    public async ValueTask<long> CountAsync(CancellationToken cancellationToken = default) =>
        await _collection.CountDocumentsAsync(_ => true, cancellationToken: cancellationToken);

    public async ValueTask<long> CountAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default
    ) => await _collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

    public async ValueTask CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    ) => await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);

    public async ValueTask Delete(TEntity entity, CancellationToken cancellationToken = default) =>
        await _collection.DeleteOneAsync(e => e.Id == entity.Id, cancellationToken);

    public async ValueTask<TEntity> FindAsync(
        string id,
        CancellationToken cancellationToken = default
    ) => await _collection.Find(e => e.Id == id).FirstOrDefaultAsync(cancellationToken);

    public async ValueTask<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default
    ) => await _collection.Find(filter).ToListAsync(cancellationToken);

    public async ValueTask Update(TEntity entity, CancellationToken cancellationToken = default) =>
        await _collection.ReplaceOneAsync(
            filter: x => x.Id == entity.Id,
            replacement: entity,
            cancellationToken: cancellationToken
        );
}
