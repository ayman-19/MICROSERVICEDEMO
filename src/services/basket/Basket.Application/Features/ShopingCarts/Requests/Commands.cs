namespace Basket.Application.Features.ShopingCarts.Requests;

public sealed record DeleteShopingCartByIdCommand(Guid Id) : IRequest<ResponseOf<ShopingCartDto>>;

public sealed record CreateShopingCartCommand : IRequest<ResponseOf<ShopingCartDto>>
{
    public string UserName { get; set; }
    public List<CreateShopingCartItemCommand> ShopingCartItems { get; set; } = new();
}

public sealed record CreateShopingCartItemCommand
{
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductImage { get; set; }
}

public sealed record UpdateShopingCartCommand : IRequest<ResponseOf<ShopingCartDto>>
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public List<UpdateShopingCartItemCommand> ShopingCartItems { get; set; } = new();
}

public sealed record UpdateShopingCartItemCommand
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductImage { get; set; }
}
