namespace Catalog.Application.Features.Products.Requests;

public sealed record PaginateProductsQuery
    : PaginateRequest,
        IRequest<PaginationResponse<IEnumerable<ProductDto>>>;

public sealed record GetProductByIdQuery(string Id) : IRequest<ResponseOf<ProductDto>>;
