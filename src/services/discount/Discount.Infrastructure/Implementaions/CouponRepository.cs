namespace Discount.Infrastructure.Implementaions;

internal sealed class CouponRepository(NpgsqlConnection connection) : ICouponRepository
{
    public async ValueTask<bool> AnyAsync(long id, CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT 1
            FROM Coupon
            WHERE id = @Id
            LIMIT 1;
            """;

        //await using var connection = CreateConnection();

        var result = await connection.ExecuteScalarAsync<int?>(
            new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken)
        );

        return result.HasValue;
    }

    public async ValueTask<bool> AnyAsync(
        string name,
        CancellationToken cancellationToken = default
    )
    {
        const string sql = """
            SELECT 1
            FROM Coupon
            WHERE ProductName = @ProductName
            LIMIT 1;
            """;

        //await using var connection = CreateConnection();

        var result = await connection.ExecuteScalarAsync<int?>(
            new CommandDefinition(
                sql,
                new { ProductName = name },
                cancellationToken: cancellationToken
            )
        );

        return result.HasValue;
    }

    public async ValueTask<Coupon> CreateAsync(
        Coupon coupon,
        CancellationToken cancellationToken = default
    )
    {
        const string sql = """
            INSERT INTO Coupon
            (
                ProductName,
                Description,
                Amount
            )
            VALUES
            (
                @ProductName,
                @Description,
                @Amount
            )
            RETURNING
                Id,
                ProductName,
                Description,
                Amount;
            """;

        //await using var connection = CreateConnection();

        var createdCoupon = await connection.QuerySingleAsync<Coupon>(
            new CommandDefinition(sql, coupon, cancellationToken: cancellationToken)
        );

        return createdCoupon;
    }

    public async ValueTask<Coupon> DeleteAsync(
        Coupon coupon,
        CancellationToken cancellationToken = default
    )
    {
        const string sql = """
            DELETE FROM Coupon
            WHERE Id = @Id
            RETURNING
                Id,
                ProductName,
                Description,
                Amount;
            """;

        //await using var connection = CreateConnection();

        var deletedCoupon = await connection.QuerySingleAsync<Coupon>(
            new CommandDefinition(sql, new { coupon.Id }, cancellationToken: cancellationToken)
        );

        return deletedCoupon;
    }

    public async ValueTask<Coupon> GetAsync(long id, CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT
                Id,
                ProductName,
                Description,
                Amount
            FROM Coupon
            WHERE id = @Id;
            """;

        //await using var connection = CreateConnection();

        var coupon = await connection.QuerySingleAsync<Coupon>(
            new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken)
        );

        return coupon;
    }

    public async ValueTask<Coupon> GetAsync(
        string name,
        CancellationToken cancellationToken = default
    )
    {
        const string sql = """
            SELECT
                Id,
                ProductName,
                Description,
                Amount
            FROM Coupon
            WHERE ProductName = @ProductName;
            """;

        //await using var connection = CreateConnection();

        var coupon = await connection.QuerySingleAsync<Coupon>(
            new CommandDefinition(
                sql,
                new { ProductName = name },
                cancellationToken: cancellationToken
            )
        );

        return coupon;
    }

    public async ValueTask<IEnumerable<Coupon>> PaginateAsync(
        int pageIndex,
        int pageSize,
        string search,
        CancellationToken cancellationToken = default
    )
    {
        const string sql = """
            SELECT 
                   ProductName,
                   Description,
                   Amount
            FROM Coupon
            WHERE (@Search IS NULL OR ProductName ILIKE '%' || @Search || '%')
            ORDER BY ProductName
            OFFSET @Offset ROWS
            FETCH NEXT @PageSize ROWS ONLY;
            """;

        //await using var connection = CreateConnection();

        var result = await connection.QueryAsync<Coupon>(
            new CommandDefinition(
                sql,
                new
                {
                    Search = string.IsNullOrWhiteSpace(search) ? null : search,
                    Offset = (pageIndex - 1) * pageSize,
                    PageSize = pageSize,
                },
                cancellationToken: cancellationToken
            )
        );

        return result;
    }

    public async ValueTask<Coupon> UpdateAsync(
        Coupon coupon,
        CancellationToken cancellationToken = default
    )
    {
        const string sql = """
            UPDATE Coupon
            SET
                ProductName = @ProductName,
                Description = @Description,
                Amount = @Amount
            WHERE Id = @Id
            RETURNING
                Id,
                ProductName,
                Description,
                Amount;
            """;

        //await using var connection = CreateConnection();

        var updatedCoupon = await connection.QuerySingleAsync<Coupon>(
            new CommandDefinition(sql, coupon, cancellationToken: cancellationToken)
        );

        return updatedCoupon;
    }

    public async ValueTask<int> TotalCountAsync(CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT COUNT(*)
            FROM Coupon;
            """;
        var count = await connection.ExecuteScalarAsync<int>(
            new CommandDefinition(sql, cancellationToken: cancellationToken)
        );
        return count;
    }

    //private NpgsqlConnection CreateConnection() =>
    //    new NpgsqlConnection(configuration["PostgreSettings:Connection"]);
}
