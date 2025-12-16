namespace Basket.Infrastructure;

public static class Dependencies
{
    public static IServiceCollection AddInfrastructureDependencies(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddScoped(typeof(IRepository<>), typeof(Repository<>))
            .AddScoped<ICheckOutRepository, CheckOutRepository>()
            .AddScoped<IShopingCartItemRepository, ShopingCartItemRepository>()
            .AddScoped<IShopingCartRepository, ShopingCartRepository>();
        return services;
    }
}
