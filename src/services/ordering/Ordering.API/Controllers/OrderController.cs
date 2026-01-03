namespace Ordering.API.Controllers;

public sealed class OrderController(ISender sender) : BaseController
{
    [HttpPost("checkout")]
    public async Task<IActionResult> CheckoutOrder([FromBody] CheckoutOrderCommand command) =>
        Ok(await sender.Send(command));

    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] PaginateOrdersRequest request) =>
        Ok(await sender.Send(request));

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetAsync([Required] [FromRoute] long id) =>
        Ok(await sender.Send(new GetOrderByIdRequest(id)));

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteAsync([Required] [FromRoute] long id) =>
        Ok(await sender.Send(new DeleteOrderByIdCommand(id)));

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateOrderCommand command) =>
        Ok(await sender.Send(command));
}
