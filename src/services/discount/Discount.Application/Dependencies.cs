namespace Discount.Application;

public static class Depndencies
{
    public static IServiceCollection AddApplictionDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Depndencies));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Depndencies).Assembly));
        services.AddGrpc();

        return services;
    }
}
