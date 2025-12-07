namespace Catalog.Infrastructure.Data;

public static class Seeding
{
    public static void Seed<TSeed>(
        IMongoCollection<TSeed> collection,
        IEnumerable<TSeed> seedData,
        CancellationToken cancellationToken = default
    )
    {
        var isExist = collection.Find(_ => true).Any(cancellationToken);
        if (!isExist)
            collection.InsertMany(seedData, cancellationToken: cancellationToken);
    }
}
