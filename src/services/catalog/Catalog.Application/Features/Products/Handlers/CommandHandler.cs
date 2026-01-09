namespace Catalog.Application.Features.Products.Handlers;

public sealed record ProductCommandHandler(
    IProductRepository productRepository,
    IMapper mapper,
    IProductSearchRepository productSearchRepository
)
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
        {
            await productSearchRepository.DeleteAsync(request.Id, cancellationToken);
            return new() { Result = mapper.Map<ProductDto>(product) };
        }
        return new();
    }

    public async Task<ResponseOf<ProductDto>> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken
    )
    {
        var product = mapper.Map<Product>(request);
        await productRepository.CreateAsync(product, cancellationToken);

        await productSearchRepository.CreateAsync(
            new ProductSearchDocument
            {
                Id = product.Id,
                Name = product.Name,
                Summary = product.Summary,
                ImageUrl = product.ImageUrl,
                Price = (double)product.Price,
                BrandId = product.Brand.Id,
                BrandName = product.Brand.Name,
                TypeName = product.Type.Name,
                TypeId = product.Type.Id,
            },
            cancellationToken
        );
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
        {
            await productSearchRepository.UpdateAsync(
                new ProductSearchDocument
                {
                    Id = product.Id,
                    Name = product.Name,
                    Summary = product.Summary,
                    ImageUrl = product.ImageUrl,
                    Price = (double)product.Price,
                    BrandId = product.Brand.Id,
                    BrandName = product.Brand.Name,
                    TypeName = product.Type.Name,
                    TypeId = product.Type.Id,
                },
                cancellationToken
            );

            return new() { Result = mapper.Map<ProductDto>(product) };
        }
        return new();
    }
}
