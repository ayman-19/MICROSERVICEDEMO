namespace Discount.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddEndpointsApiExplorer();

        builder
            .Services.AddInfrastructureDependencies(builder.Configuration)
            .AddApplictionDependencies();

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        var app = builder.Build();
        app.MigrateDatabase<Program>();
        app.UseRouting();
        app.MapGrpcService<DiscountService>();
        app.MapGet("/", () => "Use a gRPC client to communicate with this server.");
        app.Run();
    }
}
