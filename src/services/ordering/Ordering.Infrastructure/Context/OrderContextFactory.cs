namespace Ordering.Infrastructure.Context;

public sealed class OrderContextFactory : IDesignTimeDbContextFactory<OrderDbContext>
{
    public OrderDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>();
        optionsBuilder.UseSqlServer(
            "Server=localhost,9005;Database=orderdb;User Id=sa;Password=P@ssw0rd;TrustServerCertificate=True;encrypt=false;"
        );
        return new OrderDbContext(optionsBuilder.Options);
    }
}
