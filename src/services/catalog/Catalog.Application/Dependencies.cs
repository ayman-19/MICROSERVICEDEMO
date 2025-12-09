namespace Catalog.Application;

public static class Depndencies
{
    public static IServiceCollection AddApplictionDependencies(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddAutoMapper(typeof(Depndencies));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Depndencies).Assembly));
        return services;
    }
}
