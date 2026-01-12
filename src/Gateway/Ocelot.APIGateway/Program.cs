namespace Ocelot.APIGateway;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        builder.Configuration.AddJsonFile(
            $"ocelot/ocelot.{builder.Environment.EnvironmentName}.json",
            optional: true,
            reloadOnChange: true
        );
        builder.Services.AddOcelot(builder.Configuration);
        var app = builder.Build();
        app.UseRouting();
        //app.MapOpenApi();
        app.UseAuthorization();
        app.MapGet(
            "/health",
            (ILogger<Program> logger) =>
            {
                logger.LogInformation("Ocelot gateway pinged.");
                return Results.Ok("Hello Ocelot.");
            }
        );
        app.MapControllers();
        await app.UseOcelot();
        await app.RunAsync();
    }
}
