namespace Ordering.Application.Mappers.Orders;

public sealed class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>();
        CreateMap<CheckoutOrderCommand, Order>();
        CreateMap<UpdateOrderCommand, Order>();
        CreateMap<BasketCheckedOutEvent, CheckoutOrderCommand>();
    }
}
