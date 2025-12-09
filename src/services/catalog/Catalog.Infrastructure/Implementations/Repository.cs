namespace Catalog.Infrastructure.Implementations;

internal class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{
    protected readonly IMongoCollection<TEntity> _collection;

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

    public async ValueTask<bool> CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        return true;
    }

    public async ValueTask<bool> Delete(
        TEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        var success = await _collection.DeleteOneAsync(e => e.Id == entity.Id, cancellationToken);
        return success.DeletedCount > 0 && success.IsAcknowledged;
    }

    public async ValueTask<TEntity> FindAsync(
        string id,
        CancellationToken cancellationToken = default
    ) => await _collection.Find(e => e.Id == id).FirstOrDefaultAsync(cancellationToken);

    public async ValueTask<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default
    ) => await _collection.Find(filter).ToListAsync(cancellationToken);

    public async ValueTask<bool> Update(
        TEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        var success = await _collection.ReplaceOneAsync(
            filter: x => x.Id == entity.Id,
            replacement: entity,
            cancellationToken: cancellationToken
        );
        return success.ModifiedCount > 0 && success.IsAcknowledged;
    }
}
