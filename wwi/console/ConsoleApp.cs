using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace wwi.console
{
    public class ConsoleApp : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public ConsoleApp(ILogger<ConsoleApp> logger, IMediator mediator)
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting application");
            int choice = -1;
            while (choice != 0)
            {
                Console.WriteLine("Please choose one of the following options:");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. List People");

                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    choice = -1;
                }

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Bye...");
                        break;

                    case 1:
                        var result = await _mediator.Send(new Features.People.Index.Query(), cancellationToken);
                        foreach (var person in result.People)
                        {
                            Console.WriteLine(person.FullName);
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid choice! Please try again.");
                        break;
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}