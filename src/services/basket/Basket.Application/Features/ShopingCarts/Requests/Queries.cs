namespace Basket.Application.Features.ShopingCarts.Requests;

public sealed record GetAllShopingCartsQuery : IRequest<ResponseOf<IEnumerable<ShopingCartDto>>>;

public sealed record GetShopingCartByIdQuery(Guid Id) : IRequest<ResponseOf<ShopingCartDto>>;
