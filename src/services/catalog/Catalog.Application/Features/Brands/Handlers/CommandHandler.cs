namespace Catalog.Application.Features.Brands.Handlers;

public sealed record BrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
    : IRequestHandler<CreateBrandCommand, ResponseOf<BrandDto>>,
        IRequestHandler<UpdateBrandCommand, ResponseOf<BrandDto>>,
        IRequestHandler<DeleteBrandCommand, ResponseOf<BrandDto>>
{
    public async Task<ResponseOf<BrandDto>> Handle(
        CreateBrandCommand request,
        CancellationToken cancellationToken
    )
    {
        var brand = new Brand { Name = request.Name };
        await brandRepository.CreateAsync(brand, cancellationToken);
        return new() { Result = mapper.Map<BrandDto>(brand) };
    }

    public async Task<ResponseOf<BrandDto>> Handle(
        UpdateBrandCommand request,
        CancellationToken cancellationToken
    )
    {
        var brand = await brandRepository.FindAsync(request.Id, cancellationToken);
        mapper.Map(request, brand);
        var success = await brandRepository.Update(brand, cancellationToken);
        if (success)
            return new() { Result = mapper.Map<BrandDto>(brand) };
        return new();
    }

    public async Task<ResponseOf<BrandDto>> Handle(
        DeleteBrandCommand request,
        CancellationToken cancellationToken
    )
    {
        var brand = await brandRepository.FindAsync(request.Id, cancellationToken);
        var success = await brandRepository.Delete(brand, cancellationToken);
        if (success)
            return new() { Result = mapper.Map<BrandDto>(brand) };
        return new();
    }
}
