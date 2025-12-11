namespace Catalog.API.Controllers;

public sealed class BrandController(ISender sender) : BaseController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseOf<BrandDto>>> GetAsync(
        [Required] [FromRoute] string id,
        CancellationToken cancellationToken
    ) => Ok(await sender.Send(new GetBrandByIdQuery(id), cancellationToken));

    [HttpGet]
    public async Task<ActionResult<PaginationResponse<IEnumerable<BrandDto>>>> GetAsync(
        [FromQuery] PaginateBrandsQuery query,
        CancellationToken cancellationToken
    ) => Ok(await sender.Send(query, cancellationToken));

    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseOf<BrandDto>>> DeleteAsync(
        [Required] [FromRoute] string id,
        CancellationToken cancellationToken
    ) => Ok(await sender.Send(new DeleteBrandCommand(id), cancellationToken));

    [HttpPost]
    public async Task<ActionResult<ResponseOf<BrandDto>>> CreateAsync(
        [FromBody] CreateBrandCommand command,
        CancellationToken cancellationToken
    ) => Ok(await sender.Send(command, cancellationToken));

    [HttpPut()]
    public async Task<ActionResult<ResponseOf<BrandDto>>> UpdateAsync(
        [FromBody] UpdateBrandCommand command,
        CancellationToken cancellationToken
    ) => Ok(await sender.Send(command, cancellationToken));
}
