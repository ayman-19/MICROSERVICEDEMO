namespace Basket.Application.Mappers.ShopingCarts;

public sealed class ShopingCartProfile : Profile
{
    public ShopingCartProfile()
    {
        CreateMap<ShopingCart, ShopingCartDto>().ReverseMap();
        CreateMap<ShopingCartItem, ShopingCartItemDto>().ReverseMap();
        CreateMap<CreateShopingCartCommand, ShopingCart>()
            .ForMember(
                dest => dest.ShopingCartItems,
                opt => opt.MapFrom(src => src.ShopingCartItems)
            );
        CreateMap<CreateShopingCartItemCommand, ShopingCartItem>();
        CreateMap<UpdateShopingCartCommand, ShopingCart>()
            .ForMember(dest => dest.ShopingCartItems, opt => opt.Ignore());
    }
}
