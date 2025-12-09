namespace Catalog.Application.Features.Brands.Handlers;

public sealed record BrandQuerysHandler(IBrandRepository brandRepository, IMapper mapper)
    : IRequestHandler<PaginateBrandsQuery, PaginationResponse<IEnumerable<BrandDto>>>,
        IRequestHandler<GetBrandByIdQuery, ResponseOf<BrandDto>>
{
    public async Task<PaginationResponse<IEnumerable<BrandDto>>> Handle(
        PaginateBrandsQuery request,
        CancellationToken cancellationToken
    )
    {
        var totalCount = await brandRepository.CountAsync(cancellationToken);
        var brands = await brandRepository.PaginateAsync(
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
            Count = brands.Count(),
            Result = mapper.Map<IEnumerable<BrandDto>>(brands),
        };
    }

    public async Task<ResponseOf<BrandDto>> Handle(
        GetBrandByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var brand = await brandRepository.FindAsync(request.Id, cancellationToken);
        return new() { Result = mapper.Map<BrandDto>(brand) };
    }
}
