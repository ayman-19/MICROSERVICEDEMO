namespace Basket.Application.Grpcs.Services.Implementations;

public sealed class DiscountService(DiscountProtoService.DiscountProtoServiceClient discountClient)
    : IDiscountService
{
    public async Task<DiscountModel> GetByNameAsync(
        string productName,
        CancellationToken cancellationToken = default
    ) =>
        await discountClient.GetDiscountByNameAsync(
            new GetDiscountByNameRequest { Name = productName },
            cancellationToken: cancellationToken
        );
}
