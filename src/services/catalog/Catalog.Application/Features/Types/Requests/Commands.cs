namespace Catalog.Application.Features.Types.Requests;

public sealed record CreateTypeCommand : IRequest<ResponseOf<TypeDto>>
{
    public string Name { get; set; }
}

public sealed record UpdateTypeCommand : IRequest<ResponseOf<TypeDto>>
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public sealed record DeleteTypeCommand(string Id) : IRequest<ResponseOf<TypeDto>>;
