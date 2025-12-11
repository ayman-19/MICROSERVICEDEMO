namespace Catalog.Application.Features.Products.Handlers;

public sealed record ProductQueryHandler(IProductRepository productRepository, IMapper mapper)
    : IRequestHandler<PaginateProductsQuery, PaginationResponse<IEnumerable<ProductDto>>>,
        IRequestHandler<GetProductByIdQuery, ResponseOf<ProductDto>>
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
        return new() { Result = mapper.Map<ProductDto>(product) };
    }
}
