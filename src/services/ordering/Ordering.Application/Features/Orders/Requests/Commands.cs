namespace Ordering.Application.Features.Orders.Requests;

public sealed record CheckoutOrderCommand : IRequest<ResponseOf<OrderDto>>
{
    public string UserName { get; set; }
    public double TotalPrice { get; set; }
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

public sealed record UpdateOrderCommand : IRequest<ResponseOf<OrderDto>>
{
    public long Id { get; set; }
    public string UserName { get; set; }
    public double TotalPrice { get; set; }
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

public sealed record DeleteOrderByIdCommand(long Id) : IRequest<ResponseOf<OrderDto>>;
