using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using validate_settings_section.console.ageOptions;

internal class AgeOptionsService : IHostedService
{
    private readonly IOptions<AgeOptions> _ageOptions;

    public AgeOptionsService(IOptions<AgeOptions> ageOptions)
    {
        _ageOptions = ageOptions;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(_ageOptions.Value.ToString());
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
