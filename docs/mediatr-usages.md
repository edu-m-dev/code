---
title: "MediatR - decoupling, behavior pipelines and testing"
date: 2026-02-11
---

# Why do we need MediatR

## 1) Decoupling callers from handlers

<img src="images/decoupling.avif" alt="Decoupling" style="width:320px;max-width:100%;height:auto;border-radius:4px;">

MediatR turns requests into simple objects and handlers into discrete classes. Callers don't need concrete service references — they only send a request and receive a response.

```csharp
// Request
public record GetCustomerQuery(int Id) : IRequest<CustomerDto>;

// Handler
public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
{
    private readonly ICustomerRepository _repo;
    public GetCustomerHandler(ICustomerRepository repo) => _repo = repo;

    public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken ct)
        => await _repo.FindAsync(request.Id, ct);
}

// Usage from a controller or service
var customer = await mediator.Send(new GetCustomerQuery(42));
```

This keeps controllers thin and enforces single-responsibility for each logical operation.

---

## 2) Cross-cutting concerns via pipeline behaviors

<img src="images/pipeline-behaviors.png" alt="Pipelines" style="width:320px;max-width:100%;height:auto;border-radius:4px;">

Use `IPipelineBehavior<TRequest,TResponse>` to run code before/after handlers (logging, validation, caching, retries) without polluting handlers.

```csharp
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest,TResponse>> _logger;
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest,TResponse>> logger) => _logger = logger;

    public async Task<TResponse> Handle(TRequest request, CancellationToken ct, RequestHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation("Handling {RequestName}", typeof(TRequest).Name);
        var response = await next();
        _logger.LogInformation("Handled {RequestName}", typeof(TRequest).Name);
        return response;
    }
}

// Register in DI
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
```

Pipeline behaviors compose around handlers, making it easy to add/remove cross-cutting features consistently.

---

## 3) Testability and clear composition

<img src="images/mediatr-testing.webp" alt="Testability" style="width:320px;max-width:100%;height:auto;border-radius:4px;">

Handlers are plain classes you can unit-test in isolation. Notifications allow easy publish/subscribe composition.

```csharp
// Notification
public record OrderPlaced(int OrderId) : INotification;

// Notification handler
public class SendOrderConfirmation : INotificationHandler<OrderPlaced>
{
    public Task Handle(OrderPlaced notification, CancellationToken ct)
    {
        // send email or queue a job
        return Task.CompletedTask;
    }
}

// Unit test example (pseudo)
[Fact]
public async Task GetCustomerHandler_ReturnsCustomer()
{
    var repo = new FakeCustomerRepository(); // arrange fake data
    var handler = new GetCustomerHandler(repo);
    var result = await handler.Handle(new GetCustomerQuery(1), CancellationToken.None);
    Assert.NotNull(result);
}
```

Because handlers are instantiated via DI, you can test them by providing test doubles and verifying behavior without wiring the whole app.

You can find an example of MediatR usage [in my repo](../../code/chores/chores.bl/getAllChores/GetAllChoresHandler.cs)