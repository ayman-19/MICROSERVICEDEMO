namespace Catalog.API.Controllers;

public sealed class TypeController(ISender sender) : BaseController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseOf<TypeDto>>> GetAsync(
        [Required] [FromRoute] string id,
        CancellationToken cancellationToken
    ) => Ok(await sender.Send(new GetTypeByIdQuery(id), cancellationToken));

    [HttpGet]
    public async Task<ActionResult<PaginationResponse<IEnumerable<TypeDto>>>> GetAsync(
        [FromQuery] PaginateTypesQuery query,
        CancellationToken cancellationToken
    ) => Ok(await sender.Send(query, cancellationToken));

    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseOf<TypeDto>>> DeleteAsync(
        [Required] [FromRoute] string id,
        CancellationToken cancellationToken
    ) => Ok(await sender.Send(new DeleteTypeCommand(id), cancellationToken));

    [HttpPost]
    public async Task<ActionResult<ResponseOf<TypeDto>>> CreateAsync(
        [FromBody] CreateTypeCommand command,
        CancellationToken cancellationToken
    ) => Ok(await sender.Send(command, cancellationToken));

    [HttpPut()]
    public async Task<ActionResult<ResponseOf<TypeDto>>> UpdateAsync(
        [FromBody] UpdateTypeCommand command,
        CancellationToken cancellationToken
    ) => Ok(await sender.Send(command, cancellationToken));
}
