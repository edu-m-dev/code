using MediatR;
using Microsoft.AspNetCore.Mvc;
using static wwi.bl.Features.People.Index;

namespace wwi.web;

[ApiController]
[Route("[controller]")]
public class PeopleController : ControllerBase
{
    private readonly IMediator _mediator;

    public PeopleController(IMediator mediator)
    {
        _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<IEnumerable<Person>> Get(string searchString, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new Query()
        {
            SearchString = searchString
        }, cancellationToken);

        return result.People;
    }
}
