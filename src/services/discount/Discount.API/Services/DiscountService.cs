using Discount.Application.Features.Coupons.Requests;

namespace Discount.API.Services;

public sealed class DiscountService(ISender sender) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<DiscountModel> CreateDiscount(
        CreateDiscountRequest request,
        ServerCallContext context
    )
    {
        var command = await sender.Send(
            new CreateDiscountCommand(request),
            context.CancellationToken
        );
        return command;
    }

    public override async Task<DiscountModel> UpdateDiscount(
        UpdateDiscountRequest request,
        ServerCallContext context
    )
    {
        var command = await sender.Send(
            new UpdateDiscountCommand(request),
            context.CancellationToken
        );
        return command;
    }

    public override async Task<DiscountModel> DeleteDiscount(
        DeleteDiscountRequest request,
        ServerCallContext context
    )
    {
        var command = await sender.Send(
            new DeleteDiscountCommand(request),
            context.CancellationToken
        );
        return command;
    }

    public override async Task<DiscountModel> GetDiscount(
        GetDiscountRequest request,
        ServerCallContext context
    )
    {
        var query = await sender.Send(
            new GetDiscountByIdQuery(request.Id),
            context.CancellationToken
        );
        return query;
    }

    public override async Task<DiscountModel> GetDiscountByName(
        GetDiscountByNameRequest request,
        ServerCallContext context
    )
    {
        var query = await sender.Send(
            new GetDiscountByNameQuery(request.Name),
            context.CancellationToken
        );
        return query;
    }

    public override async Task<PaginateDiscountsResponse> PaginateDiscounts(
        PaginateDiscountsRequest request,
        ServerCallContext context
    )
    {
        var query = await sender.Send(
            new PaginateDiscountQuery()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Search = request.Search,
            },
            context.CancellationToken
        );

        if (query is not null)
        {
            var response = new PaginateDiscountsResponse
            {
                TotalCount = (int)query.TotalCount,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Count = query.Count,
            };
            response.Discounts.AddRange(query.Result);

            return response;
        }

        return new();
    }
}
