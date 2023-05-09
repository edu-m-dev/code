using EasyNetQ;
using task_practice.messages;

const int SubscriberNumber = 10;

using var bus = RabbitHutch.CreateBus("host=localhost");

for (int i = 0; i < SubscriberNumber; i++)
{
    bus.PubSub.SubscribeAsync<PlaceOrder>($"orderPlacer", async placeOrder =>
    {
        await Task.Run(() => HandlePlaceOrderAsync(placeOrder));
    });
}

Console.WriteLine("Listening for messages. Hit <return> to quit.");
Console.ReadLine();

async Task HandlePlaceOrderAsync(PlaceOrder placeOrder)
{
    if (placeOrder is null)
    {
        throw new ArgumentNullException(nameof(placeOrder));
    }

    // expensive op
    var rnd = new Random().Next(1_000_000);
    if (IsPrime(rnd))
    {
        throw new Exception($"primer number {rnd} not allowed");
    }

    var orderPlaced = new OrderPlaced { OrderId = rnd };

    var serializedMessage = System.Text.Json.JsonSerializer.Serialize(orderPlaced);
    var thread = Thread.CurrentThread;
    Console.WriteLine($"[{thread.ManagedThreadId}] OrderPlaced message {serializedMessage} published");
    await bus.PubSub.PublishAsync(orderPlaced);
    Console.WriteLine($"[{thread.ManagedThreadId}] OrderPlaced message {serializedMessage} publisher-ack'd");
}

bool IsPrime(int number)
{
    if (number == 1) return false;
    if (number == 2) return true;

    var limit = Math.Ceiling(Math.Sqrt(number)); //hoisting the loop limit

    for (int i = 2; i <= limit; ++i)
        if (number % i == 0)
            return false;
    return true;
}
