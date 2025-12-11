namespace Catalog.Infrastructure;

public static class Dependencies
{
    public static IServiceCollection AddInfrastructureDependencies(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddScoped<CatalogDbContext>()
            .AddScoped(typeof(IRepository<>), typeof(Repository<>))
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<ITypeRepository, TypeRepository>()
            .AddScoped<IBrandRepository, BrandRepository>();

        //services.Configure<MongoSettings>(configuration.GetSection("MongoSettings"));
        return services;
    }
}
