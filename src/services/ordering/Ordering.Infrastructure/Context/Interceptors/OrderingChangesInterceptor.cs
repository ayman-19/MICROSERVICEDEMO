namespace Ordering.Infrastructure.Context.Interceptors;

public sealed class OrderingChangesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    )
    {
        var context = eventData.Context;
        if (context is null)
        {
            return ValueTask.FromResult(result);
        }

        var now = DateTime.UtcNow;

        foreach (
            var entry in context
                .ChangeTracker.Entries<ITraceableCreate>()
                .Where(e => e.State == EntityState.Added)
        )
        {
            entry.Entity.CreatedOn = now;
        }

        foreach (
            var entry in context
                .ChangeTracker.Entries<ITraceableUpdate>()
                .Where(e => e.State == EntityState.Modified)
        )
        {
            entry.Entity.UpdatedOn = now;
        }

        foreach (
            var entry in context
                .ChangeTracker.Entries<ITraceableDelete>()
                .Where(e => e.State == EntityState.Deleted)
        )
        {
            if (entry.Entity.Deleted)
                continue;

            entry.Entity.Deleted = true;
            entry.State = EntityState.Modified;
        }

        return ValueTask.FromResult(result);
    }
}
