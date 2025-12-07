namespace Catalog.Core.Entities.Products;

public sealed record Product : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Summary { get; set; }
    public string ImageUrl { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
    public decimal Price { get; set; }
    public Brand Brand { get; set; }
    public ProductType Type { get; set; }
}
