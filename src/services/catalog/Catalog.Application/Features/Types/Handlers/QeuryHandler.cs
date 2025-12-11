namespace Catalog.Application.Features.Types.Handlers;

public sealed record TypeQuerysHandler(ITypeRepository typeRepository, IMapper mapper)
    : IRequestHandler<PaginateTypesQuery, PaginationResponse<IEnumerable<TypeDto>>>,
        IRequestHandler<GetTypeByIdQuery, ResponseOf<TypeDto>>
{
    public async Task<PaginationResponse<IEnumerable<TypeDto>>> Handle(
        PaginateTypesQuery request,
        CancellationToken cancellationToken
    )
    {
        var totalCount = await typeRepository.CountAsync(cancellationToken);
        var types = await typeRepository.PaginateAsync(
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
            Count = types.Count(),
            Result = mapper.Map<IEnumerable<TypeDto>>(types),
        };
    }

    public async Task<ResponseOf<TypeDto>> Handle(
        GetTypeByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var type = await typeRepository.FindAsync(request.Id, cancellationToken);
        return new() { Result = mapper.Map<TypeDto>(type) };
    }
}
