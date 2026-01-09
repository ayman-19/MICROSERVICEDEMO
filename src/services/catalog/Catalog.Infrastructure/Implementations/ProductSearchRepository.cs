namespace Catalog.Infrastructure.Implementations;

public class ProductSearchRepository(ElasticsearchClient client) : IProductSearchRepository
{
    private const string IndexName = "products-v1";

    public async Task CreateAsync(ProductSearchDocument product, CancellationToken ct)
    {
        await client.IndexAsync(product, i => i.Index(IndexName), ct);
    }

    public Task DeleteAsync(string id, CancellationToken ct)
    {
        return client.DeleteAsync<ProductSearchDocument>(id, d => d.Index(IndexName), ct);
    }

    public async Task<ProductSearchDocument> GetByIdAsync(string id, CancellationToken ct)
    {
        var response = await client.GetAsync<ProductSearchDocument>(
            id,
            i => i.Index(IndexName),
            ct
        );
        return response.Found ? response.Source : null;
    }

    public async Task<IEnumerable<ProductSearchDocument>> SearchAsync(
        string query,
        int page,
        int size,
        CancellationToken ct
    )
    {
        #region
        //var response = await client.SearchAsync<ProductSearchDocument>(
        //	s =>
        //		s.Indices(IndexName)
        //			.From((page - 1) * size)
        //			.Size(size)
        //			.Query(q =>
        //				q.MultiMatch(m =>
        //					m.Fields(x => x.Name, x => x.Summary, x => x.BrandName, x => x.TypeName)
        //						.Query(query)
        //				)
        //			),
        //	ct
        //);
        #endregion

        var response = await client.SearchAsync<ProductSearchDocument>(
            s =>
                s.Indices(IndexName)
                    .From((page - 1) * size)
                    .Size(size)
                    .Query(q =>
                        q.MultiMatch(m =>
                            m.Fields(new[] { "name", "summary", "brandName", "typeName" })
                                .Query(query)
                                .PrefixLength(1)
                                .MaxExpansions(50)
                        )
                    ),
            ct
        );

        return response.Hits.Select(h => h.Source);
    }

    public Task UpdateAsync(ProductSearchDocument product, CancellationToken ct)
    {
        return client.IndexAsync(product, i => i.Index(IndexName).Id(product.Id), ct);
    }
}
