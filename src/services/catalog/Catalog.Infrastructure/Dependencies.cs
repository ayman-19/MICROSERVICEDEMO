using System.Collections.Specialized;

namespace Catalog.Infrastructure;

public static class Dependencies
{
    //public static IServiceCollection AddInfrastructureDependencies(
    //    this IServiceCollection services,
    //    IConfiguration configuration
    //)
    //{
    //    services
    //        .AddScoped<CatalogDbContext>()
    //        .AddScoped(typeof(IRepository<>), typeof(Repository<>))
    //        .AddScoped<IProductRepository, ProductRepository>()
    //        .AddScoped<ITypeRepository, TypeRepository>()
    //        .AddScoped<IProductIndexInitializer, ProductIndexInitializer>()
    //        .AddScoped<IProductSearchRepository, ProductSearchRepository>()
    //        .AddScoped<IBrandRepository, BrandRepository>();

    //    services.AddSingleton(_ =>
    //    {
    //        var settings = new ElasticsearchClientSettings(
    //            new Uri(configuration["LoggingOptions:ElasticUrl"]!)
    //        ).DefaultIndex("products-v1");

    //        return new ElasticsearchClient(settings);
    //    });

    //    services.AddHostedService<ElasticIndexHostedService>();

    //    //services.Configure<MongoSettings>(configuration.GetSection("MongoSettings"));
    //    return services;
    //}

    public static IServiceCollection AddInfrastructureDependencies(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        // Database
        services.AddScoped<CatalogDbContext>();

        // Repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ITypeRepository, TypeRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();

        // Elastic
        services.AddScoped<IProductIndexInitializer, ProductIndexInitializer>();
        services.AddScoped<IProductSearchRepository, ProductSearchRepository>();

        services.AddSingleton(_ =>
        {
            var headers = new NameValueCollection
            {
                { "Accept", "application/vnd.elasticsearch+json;compatible-with=8" },
                { "Content-Type", "application/vnd.elasticsearch+json;compatible-with=8" },
            };

            var settings = new ElasticsearchClientSettings(
                new Uri(configuration["LoggingOptions:ElasticUrl"]!)
            )
                .DisableMetaHeader()
                .GlobalHeaders(headers)
                .DefaultIndex("products-v1");

            return new ElasticsearchClient(settings);
        });

        services.AddSingleton(_ =>
        {
            var settings = new ElasticsearchClientSettings(
                new Uri(configuration["LoggingOptions:ElasticUrl"]!)
            ).DefaultIndex("products-v1");

            return new ElasticsearchClient(settings);
        });

        // Hosted services
        services.AddHostedService<ElasticIndexHostedService>();
        //services.Configure<MongoSettings>(configuration.GetSection("MongoSettings"));
        return services;
    }
}
