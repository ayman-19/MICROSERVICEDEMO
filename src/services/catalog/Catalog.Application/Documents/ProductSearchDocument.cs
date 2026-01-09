namespace Catalog.Application.Documents;

public sealed class ProductSearchDocument
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public double Price { get; set; }

    public string ImageUrl { get; set; } = default!;

    public string BrandId { get; set; } = default!;
    public string BrandName { get; set; } = default!;

    public string TypeId { get; set; } = default!;
    public string TypeName { get; set; } = default!;
}
