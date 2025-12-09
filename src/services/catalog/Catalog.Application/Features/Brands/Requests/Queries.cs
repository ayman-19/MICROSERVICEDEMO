namespace Catalog.Application.Features.Brands.Requests;

public sealed record PaginateBrandsQuery
    : PaginateRequest,
        IRequest<PaginationResponse<IEnumerable<BrandDto>>>;

public sealed record GetBrandByIdQuery(string Id) : IRequest<ResponseOf<BrandDto>>;
