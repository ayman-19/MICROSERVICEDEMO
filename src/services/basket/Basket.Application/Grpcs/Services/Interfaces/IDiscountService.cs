namespace Basket.Application.Grpcs.Services.Interfaces;

public interface IDiscountService
{
    Task<DiscountModel> GetByNameAsync(
        string productName,
        CancellationToken cancellationToken = default
    );
}
