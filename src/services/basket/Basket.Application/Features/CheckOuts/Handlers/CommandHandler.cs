namespace Basket.Application.Features.CheckOuts.Handlers;

public sealed record CheckOutCommandHandler(
    IShopingCartRepository shopingCartRepository,
    IBus bus,
    IMapper mapper,
    ILogger<CheckOutCommandHandler> logger
) : IRequestHandler<CheckOutCommand, ResponseOf<ShopingCartDto>>
{
    public async Task<ResponseOf<ShopingCartDto>> Handle(
        CheckOutCommand request,
        CancellationToken cancellationToken
    )
    {
        var cart = await shopingCartRepository.FindAsync(request.UserId, cancellationToken);

        if (cart is null)
            return new()
            {
                Message = "Shoping cart not found.",
                StatusCode = (int)HttpStatusCode.NotFound,
                Success = false,
            };

        var eventMessage = mapper.Map<BasketCheckedOutEvent>(request);
        eventMessage.TotalPrice = cart.ShopingCartItems.Sum(i => i.Price * i.Quantity);
        eventMessage.UserName = cart.UserName;
        try
        {
            var sendEndpoint = await bus.GetSendEndpoint(
                new Uri($"queue:{RabbitMqConstants.BasketCheckedOutQueue}")
            );
            logger.LogInformation(
                "Sending message to RabbitMQ. EventType: {EventType}, MessageId: {MessageId}",
                eventMessage.GetType().Name,
                eventMessage.CorrelationId
            );
            await sendEndpoint.Send(eventMessage, cancellationToken);
            logger.LogInformation(
                "Message published successfully. EventType: {EventType}, MessageId: {MessageId}",
                eventMessage.GetType().Name,
                eventMessage.CorrelationId
            );

            await shopingCartRepository.Delete(cart, cancellationToken);

            return new()
            {
                Result = mapper.Map<ShopingCartDto>(cart),
                Success = true,
                Message = "Checkout completed successfully",
            };
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Failed to publish message to RabbitMQ. EventType: {EventType}, MessageId: {MessageId}",
                eventMessage.GetType().Name,
                eventMessage.CorrelationId
            );
            return new();
        }
    }
}
