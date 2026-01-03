namespace Ordering.Core.Abstractions;

public interface ITraceableUpdate
{
    DateTime? UpdatedOn { get; set; }
}
