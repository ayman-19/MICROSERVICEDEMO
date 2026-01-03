namespace Ordering.Infrastructure.Implementaions;

internal sealed class UnitOfWork(OrderDbContext _context) : IUnitOfWork
//IDisposable,
//IAsyncDisposable
{
    public IExecutionStrategy CreateExecutionStrategy() =>
        _context.Database.CreateExecutionStrategy();

    public int SaveChanges() => _context.SaveChanges();

    public bool SaveChanges(int expectedModifiedRows)
    {
        var modifiedRows = _context.SaveChanges();
        return modifiedRows == expectedModifiedRows;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);

    public async Task<bool> SaveChangesAsync(
        int expectedModifiedRows,
        CancellationToken cancellationToken = default
    )
    {
        var modifiedRows = await _context.SaveChangesAsync();
        return modifiedRows == expectedModifiedRows;
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(
        CancellationToken cancellationToken = default
    ) => _context.Database.BeginTransactionAsync(cancellationToken);

    public IDbContextTransaction BeginTransaction() => _context.Database.BeginTransaction();

    public Task<IDbContextTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel,
        CancellationToken cancellationToken = default
    ) => _context.Database.BeginTransactionAsync(isolationLevel, cancellationToken);

    public IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel) =>
        _context.Database.BeginTransaction(isolationLevel);

    public ValueTask DisposeAsync() => _context.DisposeAsync();

    public void Dispose() => _context.Dispose();
}
