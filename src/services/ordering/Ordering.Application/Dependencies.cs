namespace Ordering.Application;

public static class Depndencies
{
    public static IServiceCollection AddApplictionDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Depndencies));
        services.AddValidatorsFromAssembly(typeof(Depndencies).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Depndencies).Assembly));
        return services;
    }
}
