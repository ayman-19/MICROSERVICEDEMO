namespace Catalog.Application.Features.Types.Requests;

public sealed record PaginateTypesQuery
    : PaginateRequest,
        IRequest<PaginationResponse<IEnumerable<TypeDto>>>;

public sealed record GetTypeByIdQuery(string Id) : IRequest<ResponseOf<TypeDto>>;
