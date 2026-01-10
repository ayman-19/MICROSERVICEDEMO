namespace Basket.API.Controllers.v2;

[ApiVersion("2")]
public class ShopingCartController(ISender sender) : BaseController
{
    [HttpPost("checkout")]
    public async Task<ActionResult<ResponseOf<ShopingCartDto>>> CheckoutAsync(
        [FromBody] CheckOutCommandV2 command,
        CancellationToken cancellationToken
    ) => Ok(await sender.Send(command, cancellationToken));
}
