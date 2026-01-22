namespace Orders.Models;

public record Order(DateTime Date, IEnumerable<OrderItem> Items);
