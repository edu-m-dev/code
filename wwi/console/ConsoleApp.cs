using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using wwi.bl.EF;

namespace wwi.console
{
    public class ConsoleApp : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private readonly WwiDbContext _wwiDbContext;

        public ConsoleApp(ILogger<ConsoleApp> logger, IMediator mediator, WwiDbContext wwiDbContext)
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
            _wwiDbContext = wwiDbContext ?? throw new System.ArgumentNullException(nameof(wwiDbContext));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var people = _wwiDbContext.People;
            foreach (var person in people)
            {
                Console.WriteLine($"{ person.FullName}");
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}