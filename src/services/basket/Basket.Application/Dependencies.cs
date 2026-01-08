namespace Basket.Application;

public static class Depndencies
{
    public static IServiceCollection AddApplictionDependencies(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddAutoMapper(typeof(Depndencies));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Depndencies).Assembly));
        services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(cfg =>
            cfg.Address = new Uri(configuration["GrpcSettings:DiscountUrl"])
        );
        services.AddScoped<IDiscountService, DiscountService>();

        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq(
                (ctx, cfg) =>
                {
                    cfg.Host(configuration["RabbitMQSetting:HostAddress"]);
                }
            );
        });
        services.AddMassTransitHostedService();

        return services;
    }
}
