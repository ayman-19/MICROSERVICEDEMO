namespace Catalog.Application.Features.Products.Handlers;

public sealed record ProductQueryHandler(
    IProductRepository productRepository,
    IMapper mapper,
    ILogger<ProductQueryHandler> logger,
    IProductSearchRepository productSearchRepository
)
    : IRequestHandler<PaginateProductsQuery, PaginationResponse<IEnumerable<ProductDto>>>,
        IRequestHandler<PaginateProductsV2Query, PaginationResponse<IEnumerable<ProductDto>>>,
        IRequestHandler<GetProductByIdQuery, ResponseOf<ProductDto>>,
        IRequestHandler<GetProductByIdV2Query, ResponseOf<ProductDto>>
{
    public async Task<PaginationResponse<IEnumerable<ProductDto>>> Handle(
        PaginateProductsQuery request,
        CancellationToken cancellationToken
    )
    {
        var totalCount = await productRepository.CountAsync(cancellationToken);
        var products = await productRepository.PaginateAsync(
            request.PageIndex,
            request.PageSize,
            request.Search,
            request.SortDirection,
            cancellationToken
        );

        return new()
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            Count = products.Count(),
            Result = mapper.Map<IEnumerable<ProductDto>>(products),
        };
    }

    public async Task<ResponseOf<ProductDto>> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var product = await productRepository.FindAsync(request.Id, cancellationToken);
        logger.LogInformation(
            "Product with ID {ProductId} retrieved: {@Product}",
            request.Id,
            product
        );

        return new() { Result = mapper.Map<ProductDto>(product) };
    }

    public async Task<ResponseOf<ProductDto>> Handle(
        GetProductByIdV2Query request,
        CancellationToken cancellationToken
    )
    {
        var product = await productSearchRepository.GetByIdAsync(request.Id, cancellationToken);
        return new()
        {
            Result = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = string.Empty,
                Summary = product.Summary,
                ImageUrl = product.ImageUrl,
                Price = Convert.ToDecimal(product.Price),
                Brand = new BrandDto { Id = product.BrandId, Name = product.BrandName },
                Type = new TypeDto { Id = product.TypeId, Name = product.TypeName },
            },
        };
    }

    public async Task<PaginationResponse<IEnumerable<ProductDto>>> Handle(
        PaginateProductsV2Query request,
        CancellationToken cancellationToken
    )
    {
        var products = await productSearchRepository.SearchAsync(
            request.Search,
            request.PageIndex,
            request.PageSize,
            cancellationToken
        );

        return new()
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Count = products.Count(),
            Result = products.Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = string.Empty,
                Summary = product.Summary,
                ImageUrl = product.ImageUrl,
                Price = Convert.ToDecimal(product.Price),
                Brand = new BrandDto { Id = product.BrandId, Name = product.BrandName },
                Type = new TypeDto { Id = product.TypeId, Name = product.TypeName },
            }),
        };
    }
}
