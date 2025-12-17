namespace Basket.Infrastructure;

public static class Dependencies
{
    public static IServiceCollection AddInfrastructureDependencies(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var redisConnection = configuration["CacheSettings:Connection"];

        services
            .AddStackExchangeRedisCache(cfg => cfg.Configuration = redisConnection)
            .AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var config = ConfigurationOptions.Parse(redisConnection);
                config.AbortOnConnectFail = false;
                return ConnectionMultiplexer.Connect(config);
            })
            .AddScoped(typeof(IRepository<>), typeof(Repository<>))
            .AddScoped<ICheckOutRepository, CheckOutRepository>()
            .AddScoped<IShopingCartItemRepository, ShopingCartItemRepository>()
            .AddScoped<IShopingCartRepository, ShopingCartRepository>();
        return services;
    }
}
