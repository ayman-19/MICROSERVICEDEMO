namespace Basket.Core.Entities.ShopingCarts;

public sealed record ShopingCart : Entity
{
    public string UserName { get; set; }
    public List<ShopingCartItem> ShopingCartItems { get; set; } = new();
}
