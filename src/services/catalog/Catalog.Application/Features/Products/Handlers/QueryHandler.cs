namespace Catalog.Application.Features.Products.Handlers;

public sealed record PaginateProductsHandler(IProductRepository productRepository, IMapper mapper)
    : IRequestHandler<PaginateProductsQuery, PaginationResponse<IEnumerable<ProductDto>>>
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
}
