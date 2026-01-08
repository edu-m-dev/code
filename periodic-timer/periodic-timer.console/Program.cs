using periodic_timer.console;

var tasks = Enumerable.Range(1, 10)
        .Select(x =>
         {
             var cts = new CancellationTokenSource();
             cts.CancelAfter(TimeSpan.FromSeconds(x));
             return (cts, timer: new CancellablePeriodicTimer(x.ToString(), TimeSpan.FromSeconds(1), cts.Token));
         })
    .Select(async t =>
{
    await t.timer.StartAsync();
});

await Task.WhenAll(tasks);
Console.WriteLine("all timers stopped");
