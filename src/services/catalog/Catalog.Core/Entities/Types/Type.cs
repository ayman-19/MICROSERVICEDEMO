namespace Catalog.Core.Entities.Types;

public sealed record ProductType : Entity
{
    public string Name { get; set; }
}
