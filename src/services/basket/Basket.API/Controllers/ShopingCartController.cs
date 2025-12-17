namespace Basket.API.Controllers;

public sealed class ShopingCartController(ISender sender) : BaseController
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
    public async Task<ActionResult<ResponseOf<IEnumerable<ShopingCartDto>>>> GetBAsync(
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
}
