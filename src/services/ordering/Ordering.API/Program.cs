namespace Ordering.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder
            .Services.AddInfrastructureDependencies(builder.Configuration)
            .AddApplictionDependencies()
            .AddScoped<ExceptionHandler>();
        builder.Services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 10);
            options.ReportApiVersions = true;
        });
        builder.Services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "Ordering API",
                    Version = "v1",
                    Description = "Ordering API for E-Commerce Application built by microservice",
                    Contact = new OpenApiContact
                    {
                        Name = "Ayman Roshdy",
                        Email = "ayman.rooshdy@gmail.com",
                    },
                }
            );
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
        builder.Services.AddControllers();
        //builder.Services.AddOpenApi();
        var app = builder.Build();
        app.UseMiddleware<ExceptionHandler>();
        app.MigrateDatabase<OrderDbContext>();
        //app.MapOpenApi();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.EnableFilter());
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
