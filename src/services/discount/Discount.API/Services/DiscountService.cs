namespace Discount.API.Services;

public sealed class DiscountService(ISender sender) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<DiscountModel> CreateDiscount(
        CreateDiscountRequest request,
        ServerCallContext context
    )
    {
        var command = await sender.Send(request, context.CancellationToken);
        return command as DiscountModel ?? new();
    }

    public override async Task<DiscountModel> UpdateDiscount(
        UpdateDiscountRequest request,
        ServerCallContext context
    )
    {
        var command = await sender.Send(request, context.CancellationToken);
        return command as DiscountModel ?? new();
    }

    public override async Task<DiscountModel> DeleteDiscount(
        DeleteDiscountRequest request,
        ServerCallContext context
    )
    {
        var command = await sender.Send(request, context.CancellationToken);
        return command as DiscountModel ?? new();
    }

    public override async Task<DiscountModel> GetDiscount(
        GetDiscountRequest request,
        ServerCallContext context
    )
    {
        var query = await sender.Send(request, context.CancellationToken);
        return query as DiscountModel ?? new();
    }

    public override async Task<PaginateDiscountsResponse> PaginateDiscounts(
        PaginateDiscountsRequest request,
        ServerCallContext context
    )
    {
        var query = await sender.Send(request, context.CancellationToken);

        var result = query as PaginationResponse<IEnumerable<DiscountModel>>;
        if (result is not null)
        {
            var response = new PaginateDiscountsResponse
            {
                TotalCount = (int)result.TotalCount,
                PageIndex = result.PageIndex,
                PageSize = result.PageSize,
                Count = result.Count,
            };
            response.Discounts.AddRange(result.Result);

            return response;
        }

        return new();
    }
}
