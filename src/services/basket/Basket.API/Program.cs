namespace Basket.API;

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
                    Title = "Basket API",
                    Version = "v1",
                    Description = "Basket API for E-Commerce Application built by microservice",
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
        builder.Services.AddOpenApi();
        var app = builder.Build();
        app.UseExceptionHandler("/error");
        app.UseHsts();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.EnableFilter());
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
