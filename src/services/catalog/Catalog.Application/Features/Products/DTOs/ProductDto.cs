namespace Catalog.Application.Features.Products.DTOs;

public sealed record ProductDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Summary { get; set; }
    public string ImageUrl { get; set; }

    public decimal Price { get; set; }
    public BrandDto Brand { get; set; }
    public TypeDto Type { get; set; }
}
