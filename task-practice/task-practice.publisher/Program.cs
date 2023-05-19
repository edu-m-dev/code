using EasyNetQ;
using task_practice.messages;

using var bus = RabbitHutch.CreateBus("host=localhost");

var input = string.Empty;
Console.WriteLine("Enter a message. 'Quit' to quit.");
while (!(input = Console.ReadLine() ?? "").Equals("Quit", StringComparison.InvariantCultureIgnoreCase))
{
    if (!int.TryParse(input, out var orderNumber))
    {
        orderNumber = 100;
    }

    var messages = Enumerable.Range(1, orderNumber)
                    .Select(x => new PlaceOrder
                    {
                        CorrelationId = Guid.NewGuid(),
                        Products = Enumerable.Range(1, 1)
                         .Select(p => new Product { Id = p })
                         .ToList(),
                    })
                    .ToList();

    foreach (var x in messages)
    {
        var serializedMessage = System.Text.Json.JsonSerializer.Serialize(x);
        Console.WriteLine($"PlaceOrder message {serializedMessage} published");
        await bus.PubSub.PublishAsync(x);
        Console.WriteLine($"PlaceOrder message {serializedMessage} publisher-ack'd");
    }
}
