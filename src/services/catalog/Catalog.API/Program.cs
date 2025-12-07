using System.Text.Json;
using Catalog.Application;
using Catalog.Application.Features.Products.Requests;
using Catalog.Core.Entities.Brands;
using Catalog.Core.Entities.Products;
using Catalog.Core.Entities.Types;
using Catalog.Infrastructure;
using Catalog.Infrastructure.Data;
using MediatR;

namespace Catalog.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

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
            .RegisterApplictionDependencies(builder.Configuration);

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        var app = builder.Build();
        app.MapOpenApi();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        #region seeding
        var env = app.Services.GetRequiredService<IWebHostEnvironment>();
        var basePath = Path.Combine(env.WebRootPath, "Seeds");
        var _database = app.Services.GetRequiredService<CatalogDbContext>();
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
        app.MapGet(
            "/products",
            async (
                ISender sender,
                int pageIndex,
                int pageSize,
                string? search,
                CancellationToken ct
            ) =>
                await sender.Send(
                    new PaginateProductsQuery
                    {
                        PageIndex = pageIndex,
                        PageSize = pageSize,
                        Search = search,
                    },
                    ct
                )
        );
        app.MapGet(
            "/brands",
            async (IBrandRepository brand, CancellationToken ct) =>
                await brand.GetAllAsync(p => true, ct)
        );
        app.MapGet(
            "/types",
            async (ITypeRepository type, CancellationToken ct) =>
                await type.GetAllAsync(p => true, ct)
        );
        app.MapDelete(
            "/products/{id}",
            async (IProductRepository product, string id, CancellationToken ct) =>
            {
                var prod = await product.FindAsync(id, ct);

                await product.Delete(prod, ct);

                return Results.Ok(prod);
            }
        );
        app.MapPost(
            "/products",
            async (IProductRepository productRepository, Product product, CancellationToken ct) =>
            {
                await productRepository.CreateAsync(product, ct);
                return Results.Created($"/products/{product.Id}", product);
            }
        );
        app.Run();
    }
}
