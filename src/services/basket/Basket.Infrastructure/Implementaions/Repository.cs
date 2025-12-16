namespace Basket.Infrastructure.Implementaions;

internal class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{
    readonly IConnectionMultiplexer multiplexer;
    readonly IDistributedCache cache;

    public Repository(IConnectionMultiplexer multiplexer, IDistributedCache cache)
    {
        this.multiplexer = multiplexer;
        this.cache = cache;
    }

    public async ValueTask<bool> AnyAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await FindAsync(id, cancellationToken) != default;
    }

    public async ValueTask<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await Task.Run(
            () =>
                multiplexer
                    .GetServer(multiplexer.GetEndPoints().First())
                    .Keys(pattern: $"{typeof(TEntity).Name}-*")
                    .Count()
        );
    }

    public async ValueTask<bool> CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        string key = GenerateKey<TEntity>(entity.Id.ToString());
        await cache.SetStringAsync(key, JsonConvert.SerializeObject(entity));
        return true;
    }

    public async ValueTask<bool> Delete(
        TEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        string key = GenerateKey<TEntity>(entity.Id.ToString());
        await cache.RemoveAsync(key);
        return true;
    }

    public async ValueTask<TEntity> FindAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        string key = GenerateKey<TEntity>(id.ToString());
        var entity = await cache.GetStringAsync(key);
        if (string.IsNullOrEmpty(entity))
            return default;
        return JsonConvert.DeserializeObject<TEntity>(entity);
    }

    public async ValueTask<IEnumerable<TEntity>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        var keys = multiplexer
            .GetServer(multiplexer.GetEndPoints().First())
            .Keys(pattern: $"{typeof(TEntity).Name}-*")
            .Select(x => x.ToString());

        var entities = new List<TEntity>();

        foreach (var key in keys)
        {
            var entity = await cache.GetStringAsync(key);
            if (!string.IsNullOrEmpty(entity))
            {
                entities.Add(JsonConvert.DeserializeObject<TEntity>(entity));
            }
        }
        return entities;
    }

    public async ValueTask<bool> Update(
        TEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        string key = GenerateKey<TEntity>(entity.Id.ToString());
        await cache.RemoveAsync(key);
        await CreateAsync(entity, cancellationToken);
        return true;
    }

    static string GenerateKey<T>(string id) => $"{typeof(T).Name}-{id}";
}
