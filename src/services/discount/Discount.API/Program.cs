using Microsoft.AspNetCore.Server.Kestrel.Core;

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
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(
                8082,
                listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http2;
                }
            );
        });

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
