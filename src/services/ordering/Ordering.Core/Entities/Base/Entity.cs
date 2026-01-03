namespace Ordering.Core.Entities.Base;

public abstract record Entity
{
    protected Entity() { }

    public long Id { get; set; }
}
