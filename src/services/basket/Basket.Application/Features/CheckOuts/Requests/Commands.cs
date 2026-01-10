namespace Basket.Application.Features.CheckOuts.Requests;

public sealed record CheckOutCommand : IRequest<ResponseOf<ShopingCartDto>>
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string CardName { get; set; }
    public string CardNumber { get; set; }
    public string Expiration { get; set; }
    public string CVV { get; set; }
    public int PaymentMethod { get; set; }
}

public sealed record CheckOutCommandV2 : IRequest<ResponseOf<ShopingCartDto>>
{
    public Guid UserId { get; set; }
    public double TotalPrice { get; set; }
}
