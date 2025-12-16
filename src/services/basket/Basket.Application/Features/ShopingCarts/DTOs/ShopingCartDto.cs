namespace Basket.Application.Features.ShopingCarts.DTOs;

public sealed record ShopingCartDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public List<ShopingCartItemDto> ShopingCartItems { get; set; } = new();
    public double TotalPrice => ShopingCartItems.Select(i => i.Price).Sum();
}
