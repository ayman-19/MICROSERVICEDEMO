namespace Ordering.Infrastructure.Context.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.HasQueryFilter(o => !o.Deleted);
    }
}
