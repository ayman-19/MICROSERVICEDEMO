namespace EventBus.RabbitMQ.Events;

public record IntegrateEvent
{
    public string CorrelationId { get; set; }
    public DateTime CreateOn { get; set; }

    public IntegrateEvent()
    {
        CorrelationId = Guid.NewGuid().ToString();
        CreateOn = DateTime.UtcNow;
    }

    public IntegrateEvent(string correlationId, DateTime createOn)
    {
        CorrelationId = correlationId;
        CreateOn = createOn;
    }
}
