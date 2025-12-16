namespace Basket.Application.Mappers.ShopingCarts;

public sealed class ShopingCartProfile : Profile
{
    public ShopingCartProfile()
    {
        CreateMap<ShopingCart, ShopingCartDto>().ReverseMap();
        CreateMap<ShopingCartItem, ShopingCartItemDto>().ReverseMap();
    }
}
