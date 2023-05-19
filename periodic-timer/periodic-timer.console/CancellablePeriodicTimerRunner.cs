namespace periodic_timer.console
{
    public class CancellablePeriodicTimer
    {
        private readonly string _name;
        private readonly TimeSpan _timeSpan;
        private readonly CancellationToken _cancellationToken;

        public CancellablePeriodicTimer(string name, TimeSpan timeSpan, CancellationToken cancellationToken)
        {
            _name = name;
            _timeSpan = timeSpan;
            _cancellationToken = cancellationToken;
        }
        public async Task StartAsync()
        {
            using PeriodicTimer timer = new(_timeSpan);
            try
            {
                while (await timer.WaitForNextTickAsync(_cancellationToken))
                {
                    Console.WriteLine($"{DateTime.Now} from {_name}");
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"timer {_name} stopped");
            }
        }
    }
}
