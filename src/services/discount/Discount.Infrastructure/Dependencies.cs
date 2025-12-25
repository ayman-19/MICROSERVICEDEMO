namespace Discount.Infrastructure;

public static class Dependencies
{
    public static IServiceCollection AddInfrastructureDependencies(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddScoped<NpgsqlConnection>(sp =>
            {
                return new NpgsqlConnection(
                    configuration.GetValue<string>("PostgreSettings:Connection")
                );
            })
            .AddScoped<ICouponRepository, CouponRepository>();

        return services;
    }
}
