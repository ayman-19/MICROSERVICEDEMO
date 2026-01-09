namespace Catalog.Infrastructure.HostedServices;

public class ElasticIndexHostedService(IServiceScopeFactory scopeFactory) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();

        var initializer = scope.ServiceProvider.GetRequiredService<IProductIndexInitializer>();

        var exists = await initializer.ExistsAsync(cancellationToken);
        if (!exists)
        {
            await initializer.CreateIndexAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
