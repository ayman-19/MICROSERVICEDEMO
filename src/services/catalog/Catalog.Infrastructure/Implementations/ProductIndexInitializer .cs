namespace Catalog.Infrastructure.Implementations;

internal class ProductIndexInitializer(ElasticsearchClient client) : IProductIndexInitializer
{
    private const string IndexName = "products-v1";

    public async Task CreateIndexAsync(CancellationToken cancellationToken = default)
    {
        var response = await client.Indices.CreateAsync<ProductSearchDocument>(
            index =>
                index
                    .Index(IndexName)
                    .Mappings(m =>
                        m.Properties(p =>
                            p.Keyword(x => x.Id)
                                .Text(x => x.Name)
                                .Text(x => x.Summary)
                                .DoubleNumber(x => x.Price)
                                .Keyword(x => x.ImageUrl)
                                .Keyword(x => x.BrandId)
                                .Text(x => x.BrandName)
                                .Keyword(x => x.TypeId)
                                .Text(x => x.TypeName)
                        )
                    ),
            cancellationToken
        );
        if (!response.IsValidResponse)
        {
            var error = response.ElasticsearchServerError;
            throw new Exception($"Failed to create products index: {error?.Error?.Reason}");
        }
    }

    public async Task<bool> ExistsAsync(CancellationToken cancellationToken = default)
    {
        var exists = await client.Indices.ExistsAsync(IndexName, cancellationToken);
        return exists.Exists;
    }
}
