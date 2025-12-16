namespace Basket.Application.Features.ShopingCarts.Handlers;

public sealed record ShopingCartQuerysHandler(
    IShopingCartRepository shopingCartRepository,
    IMapper mapper
)
    : IRequestHandler<GetAllShopingCartsQuery, ResponseOf<IEnumerable<ShopingCartDto>>>,
        IRequestHandler<GetShopingCartByIdQuery, ResponseOf<ShopingCartDto>>
{
    public async Task<ResponseOf<IEnumerable<ShopingCartDto>>> Handle(
        GetAllShopingCartsQuery request,
        CancellationToken cancellationToken
    )
    {
        var shopingCarts = await shopingCartRepository.GetAllAsync(cancellationToken);
        return new() { Result = mapper.Map<IEnumerable<ShopingCartDto>>(shopingCarts) };
    }

    public async Task<ResponseOf<ShopingCartDto>> Handle(
        GetShopingCartByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var shopingCart = await shopingCartRepository.FindAsync(request.Id, cancellationToken);
        return new() { Result = mapper.Map<ShopingCartDto>(shopingCart) };
    }
}
