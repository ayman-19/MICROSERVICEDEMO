namespace Catalog.Application.Features.Products.Requests;

public sealed record CreateProductCommand : IRequest<ResponseOf<ProductDto>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Summary { get; set; }
    public string ImageUrl { get; set; }

    public decimal Price { get; set; }
    public Brand Brand { get; set; }
    public ProductType Type { get; set; }
}

public sealed record UpdateProductCommand : IRequest<ResponseOf<ProductDto>>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Summary { get; set; }
    public string ImageUrl { get; set; }

    public decimal Price { get; set; }
    public Brand Brand { get; set; }
    public ProductType Type { get; set; }
}

public sealed record DeleteProductCommand(string Id) : IRequest<ResponseOf<ProductDto>>;
