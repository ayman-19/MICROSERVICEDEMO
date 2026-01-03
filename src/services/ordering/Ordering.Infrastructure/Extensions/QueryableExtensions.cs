namespace Ordering.Infrastructure.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int index, int size) =>
        query.Skip((index - 1) * size).Take(size);
}
