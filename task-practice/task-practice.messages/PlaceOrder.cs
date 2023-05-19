namespace task_practice.messages;

public record PlaceOrder : MessageBase
{
    public required IEnumerable<Product> Products { get; init; }
};

public record Product
{
    public required int Id { get; init; }
}
