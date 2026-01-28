using chores.bl.ef;
using chores.webapi.Handlers;
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
        var result = await _mediator.Send(new GetUserChoresQuery(User));
        return Ok(result);
    }

    [Authorize(Policy = "WriteAccess")]
    [HttpPost("addChore")]
    public async Task<IActionResult> AddChore([FromBody] Chore chore)
    {
        var result = await _mediator.Send(new AddUserChoreCommand(User, chore));
        return Ok(result);
    }
}
