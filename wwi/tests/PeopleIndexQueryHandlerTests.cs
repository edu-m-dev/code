using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Index = wwi.bl.Features.People.Index;

namespace wwi.tests;

public class PeopleIndexQueryHandlerTests : BaseTest
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    [Fact(Skip = "this test connects to the db")]
    public async Task _PeopleAreReturned()
    {
        var indexQueryHandler = TestHost.Services.GetService<IRequestHandler<Index.Query, Index.Result>>();
        var result = await indexQueryHandler.Handle(
            new Index.Query { SearchString = "Smith" },
            new CancellationTokenSource().Token);
        result.People.Count.Should().BeGreaterThan(0);
    }
}
