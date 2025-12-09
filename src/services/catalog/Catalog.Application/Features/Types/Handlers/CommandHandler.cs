namespace Catalog.Application.Features.Types.Handlers;

public sealed record TypeCommandHandler(ITypeRepository typeRepository, IMapper mapper)
    : IRequestHandler<CreateTypeCommand, ResponseOf<TypeDto>>,
        IRequestHandler<UpdateTypeCommand, ResponseOf<TypeDto>>,
        IRequestHandler<DeleteTypeCommand, ResponseOf<TypeDto>>
{
    public async Task<ResponseOf<TypeDto>> Handle(
        DeleteTypeCommand request,
        CancellationToken cancellationToken
    )
    {
        var type = await typeRepository.FindAsync(request.Id, cancellationToken);
        var success = await typeRepository.Delete(type, cancellationToken);
        if (success)
            return new() { Result = mapper.Map<TypeDto>(type) };
        return new();
    }

    public async Task<ResponseOf<TypeDto>> Handle(
        CreateTypeCommand request,
        CancellationToken cancellationToken
    )
    {
        var type = mapper.Map<ProductType>(request);
        await typeRepository.CreateAsync(type, cancellationToken);
        return new() { Result = mapper.Map<TypeDto>(type) };
    }

    public async Task<ResponseOf<TypeDto>> Handle(
        UpdateTypeCommand request,
        CancellationToken cancellationToken
    )
    {
        var type = await typeRepository.FindAsync(request.Id, cancellationToken);
        mapper.Map(request, type);
        var success = await typeRepository.Update(type, cancellationToken);
        if (success)
            return new() { Result = mapper.Map<TypeDto>(type) };
        return new();
    }
}
