namespace task_practice.messages;

public record OrderPlaced : MessageBase
{
    public required int OrderId { get; init; }
}
