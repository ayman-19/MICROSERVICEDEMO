namespace Basket.Core.Entities.ShopingCartItems;

public sealed record ShopingCartItem : Entity
{
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductImage { get; set; }
}
