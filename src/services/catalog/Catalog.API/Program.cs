namespace Catalog.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
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
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        #region seeding
        var env = app.Services.GetRequiredService<IWebHostEnvironment>();
        var basePath = Path.Combine(env.WebRootPath, "Seeds");
        using var asyncScope = app.Services.CreateAsyncScope();
        var _database = asyncScope.ServiceProvider.GetService<CatalogDbContext>();
        //var _database = app.Services.GetRequiredService<CatalogDbContext>();
        //_database.Products.DeleteMany(x => true);
        // create brands
        var brandPath = Path.Combine(basePath, "brands.json");
        var brands = JsonSerializer.Deserialize<IEnumerable<Brand>>(File.ReadAllText(brandPath));
        Seeding.Seed(
            // _database.GetCollection<Brand>(nameof(EntityType.Brands)),
            _database.Brands,
            brands ?? new List<Brand>(),
            CancellationToken.None
        );
        // create product
        var productPath = Path.Combine(basePath, "products.json");
        var products = JsonSerializer.Deserialize<IEnumerable<Product>>(
            File.ReadAllText(productPath)
        );
        Seeding.Seed(_database.Products, products ?? new List<Product>(), CancellationToken.None);
        // create types
        var typePath = Path.Combine(basePath, "types.json");
        var types = JsonSerializer.Deserialize<IEnumerable<ProductType>>(
            File.ReadAllText(typePath)
        );
        Seeding.Seed(
            _database.ProductTypes,
            types ?? new List<ProductType>(),
            CancellationToken.None
        );
        #endregion
        app.Run();
    }
}
