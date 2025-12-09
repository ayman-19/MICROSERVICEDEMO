namespace Catalog.Application.Features.Brands.Requests;

public sealed record CreateBrandCommand : IRequest<ResponseOf<BrandDto>>
{
    public string Name { get; set; }
}

public sealed record UpdateBrandCommand : IRequest<ResponseOf<BrandDto>>
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public sealed record DeleteBrandCommand(string Id) : IRequest<ResponseOf<BrandDto>>;
