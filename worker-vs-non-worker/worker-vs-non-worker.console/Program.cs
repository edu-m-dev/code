using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// await Task.Run - all code on same thread
// Task.Run not awaited - task on one thread, hosted service start on another, hosted service stop yet on another
Task.Run(async () =>
{
    var cts = new CancellationTokenSource();
    var token = cts.Token;
    var nonWorker = new NonWorker();
    await new NonWorker().StartAsync(token);
    await new NonWorker().StopAsync(token);
});

await Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddHostedService<BkgWorker>();
    })
    .Build().RunAsync();

public class Worker : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"starting worker on thread {Thread.CurrentThread.ManagedThreadId}");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"stopping worker on thread {Thread.CurrentThread.ManagedThreadId}");
        return Task.CompletedTask;
    }
}

public class NonWorker
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"starting nonWorker on thread {Thread.CurrentThread.ManagedThreadId}");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"stopping nonWorker on thread {Thread.CurrentThread.ManagedThreadId}");
        return Task.CompletedTask;
    }
}

// if IHostedService implemented, ExecuteAsync will not be executed
public class BkgWorker : BackgroundService//, IHostedService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine($"executing bkgWorker on thread {Thread.CurrentThread.ManagedThreadId}");
        return Task.CompletedTask;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"starting bkgWorker on thread {Thread.CurrentThread.ManagedThreadId}");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"stopping bkgWorker on thread {Thread.CurrentThread.ManagedThreadId}");
        return Task.CompletedTask;
    }
}
