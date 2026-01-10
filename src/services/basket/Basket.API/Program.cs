namespace Basket.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseSerilog(LoggingExtensions.Configure());
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
            opt.SwaggerDoc(
                "v2",
                new OpenApiInfo
                {
                    Title = "Basket API",
                    Version = "v2",
                    Description = "Basket API for E-Commerce Application built by microservice",
                    Contact = new OpenApiContact
                    {
                        Name = "Ayman Roshdy",
                        Email = "ayman.rooshdy@gmail.com",
                    },
                }
            );
            opt.DocInclusionPredicate(
                (docName, apiDesc) =>
                {
                    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo))
                    {
                        return false;
                    }
                    var versions = methodInfo
                        .DeclaringType?.GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);
                    return versions?.Any(v => $"v{v.ToString()}" == docName) ?? false;
                }
            );
        });
        builder
            .Services.AddInfrastructureDependencies(builder.Configuration)
            .AddApplictionDependencies(builder.Configuration);
        builder
            .Services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'v'VVV";
                opt.SubstituteApiVersionInUrl = true;
            });
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        var app = builder.Build();
        app.UseExceptionHandler("/error");
        app.UseHsts();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.EnableFilter();
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket API V1");
            c.SwaggerEndpoint("/swagger/v2/swagger.json", "Basket API V2");
        });
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
