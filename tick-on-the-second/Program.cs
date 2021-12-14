// See https://aka.ms/new-console-template for more information
using System.Reactive.Concurrency;

Action work = () => Console.WriteLine(DateTime.Now.ToLongTimeString());

Scheduler.Default.Schedule(
    // start in so many seconds
    TimeSpan.FromMilliseconds(1000 - DateTime.Now.Millisecond), 
    // then run every minute
    () => Scheduler.Default.SchedulePeriodic(TimeSpan.FromSeconds(1), work));               

Console.WriteLine("Press return to exit");
Console.ReadLine();