namespace Ordering.Application.Features.Orders.DTOs;

public sealed record OrderDto
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
    public DateTime CreatedOn { get; set; }
    public long CreatorId { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public long? UpdatorId { get; set; }
}
