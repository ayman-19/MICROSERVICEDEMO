using Catalog.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Catalog.Infrastructure.Data;

public class CatalogDbContext
{
    private readonly IMongoDatabase _database;

    public CatalogDbContext(IOptions<MongoSettings> options)
    {
        var settings = options.Value;
        var client = new MongoClient(settings.Connection);
        _database = client.GetDatabase(settings.DatabaseName);
    }

    public IMongoCollection<Product> Products =>
        _database.GetCollection<Product>(nameof(EntityType.Products));

    public IMongoCollection<Brand> Brands =>
        _database.GetCollection<Brand>(nameof(EntityType.Brands));

    public IMongoCollection<ProductType> ProductTypes =>
        _database.GetCollection<ProductType>(nameof(EntityType.ProductTypes));

    public IMongoDatabase Database => _database;
}
