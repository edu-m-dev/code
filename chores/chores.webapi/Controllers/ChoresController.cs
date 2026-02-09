using chores.core.createChore;
using chores.core.getAllChores;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chores.webapi.Controllers;

[ApiController]
[Route("api/chores")]
public class ChoresController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChoresController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Policy = "ReadAccess")]
    [HttpGet("getAllChores")]
    public async Task<IActionResult> GetAllChores()
    {
        var result = await _mediator.Send(new GetAllChoresQuery());
        return Ok(result);
    }

    [Authorize(Policy = "WriteAccess")]
    [HttpPost("createChore")]
    public async Task<IActionResult> CreateChore([FromBody] CreateChoreRequest createChoreRequest)
    {
        await _mediator.Send(createChoreRequest);
        return Ok();
    }
}
