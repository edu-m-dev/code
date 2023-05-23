using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class ConsoleHostedService : IHostedService
{
    private readonly ILogger<ConsoleHostedService> _logger;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;

    public ConsoleHostedService(ILogger<ConsoleHostedService> logger, IHostApplicationLifetime hostApplicationLifetime)
    {
        _logger = logger;
        _hostApplicationLifetime = hostApplicationLifetime;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("hosted service has started");
        _hostApplicationLifetime.ApplicationStarted.Register(() =>
        {
            _logger.LogInformation("application has started");

            Task.Run(async () =>
            {
                try
                {
                    _logger.LogInformation("you've got 5s to exit with ctrl+c");
                    await Task.Delay(5000, cancellationToken);
                    _logger.LogInformation("manually throwing from thread");
                    throw new Exception("manual exception from thread");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
                finally
                {
                    _hostApplicationLifetime.StopApplication();
                }
            }, cancellationToken);
        });

        _hostApplicationLifetime.ApplicationStopped.Register(() =>
        {
            _logger.LogInformation("application has stopped");
        });

        _hostApplicationLifetime.ApplicationStopping.Register(() =>
        {
            _logger.LogInformation("application is stopping");
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("hosted service has stopped");
        return Task.CompletedTask;
    }
}
