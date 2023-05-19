namespace task_practice.messages;

public abstract record MessageBase
{
    public required Guid CorrelationId { get; init; }
};
