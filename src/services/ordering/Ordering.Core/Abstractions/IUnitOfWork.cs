namespace Ordering.Core.Abstractions;

public interface IUnitOfWork : IAsyncDisposable, IDisposable
{
    IExecutionStrategy CreateExecutionStrategy();

    Task<IDbContextTransaction> BeginTransactionAsync(
        CancellationToken cancellationToken = default
    );
    IDbContextTransaction BeginTransaction();
    Task<IDbContextTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel,
        CancellationToken cancellationToken = default
    );
    IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel);
    int SaveChanges();
    bool SaveChanges(int expectedModifiedRows);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<bool> SaveChangesAsync(
        int expectedModifiedRows,
        CancellationToken cancellationToken = default
    );
}
