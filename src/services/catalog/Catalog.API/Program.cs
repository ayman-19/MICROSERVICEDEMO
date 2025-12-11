using Catalog.Infrastructure.Settings;

namespace Catalog.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.UseKestrel(options =>
        {
            options.ListenAnyIP(8080);
        });

        builder.WebHost.ConfigureKestrel(o =>
        {
            o.ConfigureHttpsDefaults(_ => { });
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "Catalog API",
                    Version = "v1",
                    Description = "Catalog API for E-Commerce Application built by microservice",
                    Contact = new OpenApiContact
                    {
                        Name = "Ayman Roshdy",
                        Email = "ayman.rooshdy@gmail.com",
                    },
                }
            );
        });
        builder
            .Services.AddInfrastructureDependencies(builder.Configuration)
            .AddApplictionDependencies(builder.Configuration);

        builder.Services.Configure<MongoSettings>(
            builder.Configuration.GetSection("MongoSettings")
        );

        builder.Services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 10);
            options.ReportApiVersions = true;
        });
        builder.Services.AddControllers();
        //builder.Services.AddOpenApi();
        var app = builder.Build();
        //app.MapOpenApi();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.EnableFilter());
        app.UseAuthorization();
        app.MapControllers();

        #region seeding
        var env = app.Services.GetRequiredService<IWebHostEnvironment>();
        var basePath = Path.Combine(env.WebRootPath, "Seeds");

        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();

        var brandPath = Path.Combine(basePath, "brands.json");
        var brands =
            JsonSerializer.Deserialize<IEnumerable<Brand>>(File.ReadAllText(brandPath))
            ?? Enumerable.Empty<Brand>();

        Seeding.Seed(db.Brands, brands, CancellationToken.None);

        var productPath = Path.Combine(basePath, "products.json");
        var products =
            JsonSerializer.Deserialize<IEnumerable<Product>>(File.ReadAllText(productPath))
            ?? Enumerable.Empty<Product>();

        Seeding.Seed(db.Products, products, CancellationToken.None);

        var typePath = Path.Combine(basePath, "types.json");
        var types =
            JsonSerializer.Deserialize<IEnumerable<ProductType>>(File.ReadAllText(typePath))
            ?? Enumerable.Empty<ProductType>();

        Seeding.Seed(db.ProductTypes, types, CancellationToken.None);

        #endregion
        app.Run();
    }
}
