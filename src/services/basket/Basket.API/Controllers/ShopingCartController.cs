namespace Basket.API.Controllers;

public sealed class ShopingCartController(ISender sender, IMapper mapper, IBus bus) : BaseController
{
    [HttpPost]
    public async Task<ActionResult<ResponseOf<ShopingCartDto>>> CreateAsync(
        [FromBody] CreateShopingCartCommand command,
        CancellationToken cancellationToken
    ) => Ok(await sender.Send(command, cancellationToken));

    [HttpGet("{userId}")]
    public async Task<ActionResult<ResponseOf<ShopingCartDto>>> Getsync(
        [FromRoute] Guid userId,
        CancellationToken cancellationToken
    ) => Ok(await sender.Send(new GetShopingCartByIdQuery(userId), cancellationToken));

    [HttpGet]
    public async Task<ActionResult<ResponseOf<IEnumerable<ShopingCartDto>>>> GetAsync(
        CancellationToken cancellationToken
    ) => Ok(await sender.Send(new GetAllShopingCartsQuery(), cancellationToken));

    [HttpDelete("{userId}")]
    public async Task<ActionResult<ResponseOf<ShopingCartDto>>> DeleteByUserIdAsync(
        [FromRoute] Guid userId,
        CancellationToken cancellationToken
    ) => Ok(await sender.Send(new DeleteShopingCartByIdCommand(userId), cancellationToken));

    [HttpPut]
    public async Task<ActionResult<ResponseOf<ShopingCartDto>>> UpdateAsync(
        [FromBody] UpdateShopingCartCommand command,
        CancellationToken cancellationToken
    ) => Ok(await sender.Send(command, cancellationToken));

    [HttpPost("checkout")]
    public async Task<ActionResult<ResponseOf<ShopingCartDto>>> CheckoutAsync(
        [FromBody] CheckOutCommand command,
        CancellationToken cancellationToken
    )
    {
        var cart = await sender.Send(
            new GetShopingCartByIdQuery(command.UserId),
            cancellationToken
        );

        if (cart.Result is null)
            return BadRequest("Cart not found");

        var eventMessage = mapper.Map<BasketCheckedOutEvent>(command);
        eventMessage.TotalPrice = cart.Result.TotalPrice;
        eventMessage.UserName = cart.Result.UserName;

        var sendEndpoint = await bus.GetSendEndpoint(
            new Uri($"queue:{RabbitMqConstants.BasketCheckedOutQueue}")
        );
        await sendEndpoint.Send(eventMessage, cancellationToken);

        await sender.Send(new DeleteShopingCartByIdCommand(command.UserId), cancellationToken);

        var result = new ResponseOf<ShopingCartDto>
        {
            Result = cart.Result,
            Success = true,
            Message = "Checkout completed successfully",
        };

        return Ok(result);
    }
}
