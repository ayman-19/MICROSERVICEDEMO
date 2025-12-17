namespace Basket.Application.Features.ShopingCarts.Handlers;

public sealed record ShopingCartCommandHandler(
    IShopingCartRepository shopingCartRepository,
    IMapper mapper
)
    : IRequestHandler<DeleteShopingCartByIdCommand, ResponseOf<ShopingCartDto>>,
        IRequestHandler<CreateShopingCartCommand, ResponseOf<ShopingCartDto>>,
        IRequestHandler<UpdateShopingCartCommand, ResponseOf<ShopingCartDto>>
{
    public async Task<ResponseOf<ShopingCartDto>> Handle(
        DeleteShopingCartByIdCommand request,
        CancellationToken cancellationToken
    )
    {
        var exist = await shopingCartRepository.AnyAsync(request.Id, cancellationToken);
        if (!exist)
        {
            return new()
            {
                Success = false,
                Message = "Shoping cart not found",
                StatusCode = 404,
            };
        }
        var shopingCart = await shopingCartRepository.FindAsync(request.Id, cancellationToken);
        await shopingCartRepository.Delete(shopingCart, cancellationToken);
        return new() { Result = mapper.Map<ShopingCartDto>(shopingCart) };
    }

    public async Task<ResponseOf<ShopingCartDto>> Handle(
        CreateShopingCartCommand request,
        CancellationToken cancellationToken
    )
    {
        var shopingCart = mapper.Map<ShopingCart>(request);
        await shopingCartRepository.CreateAsync(shopingCart, cancellationToken);
        return new() { Result = mapper.Map<ShopingCartDto>(shopingCart) };
    }

    public async Task<ResponseOf<ShopingCartDto>> Handle(
        UpdateShopingCartCommand request,
        CancellationToken cancellationToken
    )
    {
        var exist = await shopingCartRepository.AnyAsync(request.Id, cancellationToken);
        if (!exist)
        {
            return new()
            {
                Success = false,
                Message = "Shoping cart not found",
                StatusCode = 404,
            };
        }
        var shopingCart = await shopingCartRepository.FindAsync(request.Id, cancellationToken);
        mapper.Map(request, shopingCart);

        var existingItems = shopingCart.ShopingCartItems;

        foreach (var item in request.ShopingCartItems)
        {
            var existingItem = existingItems.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                mapper.Map(item, existingItem);
            }
            else
            {
                var newItem = mapper.Map<ShopingCartItem>(item);
                shopingCart.ShopingCartItems.Add(newItem);
            }
        }
        await shopingCartRepository.Update(shopingCart, cancellationToken);
        return new() { Result = mapper.Map<ShopingCartDto>(shopingCart) };
    }
}
