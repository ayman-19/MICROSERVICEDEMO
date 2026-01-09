namespace Catalog.API.Controllers;

public sealed class ProductController(ISender sender) : BaseController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseOf<ProductDto>>> GetAsync(
        [Required] [FromRoute] string id,
        CancellationToken cancellationToken
    ) => Ok(await sender.Send(new GetProductByIdQuery(id), cancellationToken));

    [HttpGet("v2/{id}")]
    public async Task<ActionResult<ResponseOf<ProductDto>>> GetByIdAsync(
        [Required] [FromRoute] string id,
        CancellationToken cancellationToken
    ) => Ok(await sender.Send(new GetProductByIdV2Query(id), cancellationToken));

    [HttpGet]
    public async Task<ActionResult<PaginationResponse<IEnumerable<ProductDto>>>> GetAsync(
        [FromQuery] PaginateProductsQuery query,
        CancellationToken cancellationToken
    ) => Ok(await sender.Send(query, cancellationToken));

    [HttpGet("v2")]
    public async Task<ActionResult<PaginationResponse<IEnumerable<ProductDto>>>> GetAsync(
        [FromQuery] PaginateProductsV2Query query,
        CancellationToken cancellationToken
    ) => Ok(await sender.Send(query, cancellationToken));

    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseOf<ProductDto>>> DeleteAsync(
        [Required] [FromRoute] string id,
        CancellationToken cancellationToken
    ) => Ok(await sender.Send(new DeleteProductCommand(id), cancellationToken));

    [HttpPost]
    public async Task<ActionResult<ResponseOf<ProductDto>>> CreateAsync(
        [FromBody] CreateProductCommand command,
        CancellationToken cancellationToken
    ) => Ok(await sender.Send(command, cancellationToken));

    [HttpPut()]
    public async Task<ActionResult<ResponseOf<ProductDto>>> UpdateAsync(
        [FromBody] UpdateProductCommand command,
        CancellationToken cancellationToken
    ) => Ok(await sender.Send(command, cancellationToken));
}
