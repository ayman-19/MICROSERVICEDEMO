namespace Basket.Application.Features.ShopingCarts.Handlers;

public sealed record ShopingCartCommandHandler(
    IShopingCartRepository shopingCartRepository,
    IMapper mapper
);
