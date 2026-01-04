namespace Ordering.API.Consumers;

public sealed class BasketOrderConsumer(ISender sender, IMapper mapper)
    : IConsumer<BasketCheckedOutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckedOutEvent> context)
    {
        var message = context.Message;
        var orderCommand = mapper.Map<CheckoutOrderCommand>(message);
        await sender.Send(orderCommand);
    }
}
