namespace Catalog.Infrastructure.Data;

public class CatalogDbContext
{
    private readonly IMongoDatabase _database;

    public CatalogDbContext(IConfiguration config)
    {
        var client = new MongoClient(config["MongoSettings:Connection"]);
        _database = client.GetDatabase(config["MongoSettings:DatabaseName"]);
    }

    public IMongoCollection<Product> Products =>
        _database.GetCollection<Product>(nameof(EntityType.Products));

    public IMongoCollection<Brand> Brands =>
        _database.GetCollection<Brand>(nameof(EntityType.Brands));

    public IMongoCollection<ProductType> ProductTypes =>
        _database.GetCollection<ProductType>(nameof(EntityType.ProductTypes));

    public IMongoDatabase Database => _database;
}
