namespace Catalog.Application.Features.Products.Handlers;

public sealed record ProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    : IRequestHandler<CreateProductCommand, ResponseOf<ProductDto>>,
        IRequestHandler<UpdateProductCommand, ResponseOf<ProductDto>>,
        IRequestHandler<DeleteProductCommand, ResponseOf<ProductDto>>
{
    public async Task<ResponseOf<ProductDto>> Handle(
        DeleteProductCommand request,
        CancellationToken cancellationToken
    )
    {
        var product = await productRepository.FindAsync(request.Id, cancellationToken);
        var success = await productRepository.Delete(product, cancellationToken);
        if (success)
            return new() { Result = mapper.Map<ProductDto>(product) };
        return new();
    }

    public async Task<ResponseOf<ProductDto>> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken
    )
    {
        var product = mapper.Map<Product>(request);
        await productRepository.CreateAsync(product, cancellationToken);
        return new() { Result = mapper.Map<ProductDto>(product) };
    }

    public async Task<ResponseOf<ProductDto>> Handle(
        UpdateProductCommand request,
        CancellationToken cancellationToken
    )
    {
        var product = await productRepository.FindAsync(request.Id, cancellationToken);
        mapper.Map(request, product);
        var success = await productRepository.Update(product, cancellationToken);
        if (success)
            return new() { Result = mapper.Map<ProductDto>(product) };
        return new();
    }
}
