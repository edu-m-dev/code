using MediatR;
using Microsoft.AspNetCore.Mvc;
using static wwi.bl.Features.People.Index;

namespace wwi.web;

[ApiController]
[Route("[controller]")]
public class PeopleController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PeopleController> _logger;

    public PeopleController(IMediator mediator, ILogger<PeopleController> logger)
    {
        _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<Person>> Get(string searchString, CancellationToken cancellationToken)
    {
        _logger.LogInformation(searchString);
        var result = await _mediator.Send(new Query()
        {
            SearchString = searchString
        }, cancellationToken);

        return result.People;
    }
}
