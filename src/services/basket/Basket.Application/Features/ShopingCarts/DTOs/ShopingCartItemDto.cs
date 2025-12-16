namespace Basket.Application.Features.ShopingCarts.DTOs;

public sealed record ShopingCartItemDto
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductImage { get; set; }
}
